using FastDrink.Application.Common.Interfaces;
using FastDrink.Application.Common.Models;
using FastDrink.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FastDrink.Application.Users.Queries.GetIdAddress;

public class GetIdAddressQuery : IRequest<GetIdAddressResult>
{
    public int UserId { get; set; }
}

public class GetIdAddressQueryHandler : IRequestHandler<GetIdAddressQuery, GetIdAddressResult>
{
    private readonly IApplicationDbContext _dbContext;

    public GetIdAddressQueryHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetIdAddressResult> Handle(GetIdAddressQuery request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.User.Include(x => x.Address).FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        if (user == null)
        {
            Dictionary<string, string> errors = new();
            errors.Add("Usuario no encontrado", $"El usuario con el id {request.UserId} no existe");

            return GetIdAddressResult.Failure(errors);
        }

        return GetIdAddressResult.Success(user.Address);
    }
}

public class GetIdAddressResult : Result
{
    public GetIdAddressResult(bool succeeded, IDictionary<string, string> errors, Address? address) : base(succeeded, errors)
    {
        Address = address;
    }

    public Address? Address { get; set; }

    public static GetIdAddressResult Success(Address? address)
    {
        return new GetIdAddressResult(true, new Dictionary<string, string>(), address);
    }

    public static new GetIdAddressResult Failure(IDictionary<string, string> errors)
    {
        return new GetIdAddressResult(false, errors, null);
    }
}