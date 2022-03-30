using FastDrink.Application.Common.Interfaces;
using FastDrink.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FastDrink.Application.Users.Commands;

public class AddPreferedAddressCommand : IRequest<Result>
{
    public int UserId { get; set; }
    public int AddressId { get; set; }
}

public class AddPreferedAddressCommandHandler : IRequestHandler<AddPreferedAddressCommand, Result>
{
    private readonly IApplicationDbContext _dbContext;
    public AddPreferedAddressCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Result> Handle(AddPreferedAddressCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.User.FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);

        if (user == null)
        {
            Dictionary<string, string> errors = new();

            errors.Add("Usuario no encontrado", "El usuario no existe");

            return Result.Failure(errors);
        }

        user.AddressId = request.AddressId;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
