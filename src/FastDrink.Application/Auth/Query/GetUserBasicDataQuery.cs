using AutoMapper;
using FastDrink.Application.Auth.DTOs;
using FastDrink.Application.Common.Interfaces;
using HashidsNet;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FastDrink.Application.Auth.Query;

public class GetUserBasicDataQuery : IRequest<UserDto?>
{
    public string Id { get; set; }
}

public class GetUserBasicDataQueryHandler : IRequestHandler<GetUserBasicDataQuery, UserDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IHashids _hashids;

    public GetUserBasicDataQueryHandler(IApplicationDbContext context, IMapper mapper, IHashids hashids)
    {
        _context = context;
        _mapper = mapper;
        _hashids = hashids;
    }
    public async Task<UserDto?> Handle(GetUserBasicDataQuery request, CancellationToken cancellationToken)
    {
        var id = _hashids.Decode(request.Id)[0];
        var user = await _context.User.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (user == null)
        {
            return null;
        }

        var userDto = _mapper.Map<UserDto>(user);

        userDto.Id = _hashids.Encode(user.Id);

        return userDto;
    }
}
