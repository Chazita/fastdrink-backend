using AutoMapper;
using FastDrink.Application.Auth.DTOs;
using FastDrink.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FastDrink.Application.Auth.Query;

public class GetUserBasicDataQuery : IRequest<UserDto?>
{
    public int Id { get; set; }
}

public class GetUserBasicDataQueryHandler : IRequestHandler<GetUserBasicDataQuery, UserDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetUserBasicDataQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<UserDto?> Handle(GetUserBasicDataQuery request, CancellationToken cancellationToken)
    {
        var user = await _context.User.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (user == null)
        {
            return null;
        }

        return _mapper.Map<UserDto>(user);
    }
}
