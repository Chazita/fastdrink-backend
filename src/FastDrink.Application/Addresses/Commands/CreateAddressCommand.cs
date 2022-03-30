using FastDrink.Application.Common.Interfaces;
using FastDrink.Application.Common.Models;
using MediatR;

namespace FastDrink.Application.Addresses.Commands;

public class CreateAddressCommand : IRequest<CreateAddressResult>
{
    public string Province { get; set; } = "";

    public string City { get; set; } = "";

    public string Street { get; set; } = "";

    public string Code { get; set; } = "";
}

public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, CreateAddressResult>
{
    private readonly IApplicationDbContext _dbContext;
    public CreateAddressCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CreateAddressResult> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Address address = new()
        {
            City = request.City,
            Code = request.Code,
            Province = request.Province,
            Street = request.Street,
        };

        var entryAddress = await _dbContext.Address.AddAsync(address, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return CreateAddressResult.Success(entryAddress.Entity.Id);
    }
}

public class CreateAddressResult : Result
{
    public CreateAddressResult(bool succeeded, IDictionary<string, string> errors, int addressId) : base(succeeded, errors)
    {
        AddressId = addressId;
    }

    public int AddressId { get; set; }

    public static CreateAddressResult Success(int adddressId)
    {
        return new CreateAddressResult(true, new Dictionary<string, string>(), adddressId);
    }

    public static new CreateAddressResult Failure(IDictionary<string, string> errors)
    {
        return new CreateAddressResult(false, errors, 0);
    }
}
