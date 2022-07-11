using FastDrink.Application.Common.Interfaces;
using FastDrink.Application.Common.Models;
using FastDrink.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FastDrink.Application.Auth.Commands.CreateAdmin;

public class CreateAdminCommand : IRequest<Result>
{
    public string Email { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}

public class CreateAdminCommandHandler : IRequestHandler<CreateAdminCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IAuthService _authService;

    public CreateAdminCommandHandler(IApplicationDbContext context, IAuthService service)
    {
        _context = context;
        _authService = service;
    }
    public async Task<Result> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
    {
        var userExist = await _context.User.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

        if (userExist != null)
        {
            Dictionary<string, string> errors = new();

            errors.Add("Email", "Ya existe un usuario con este correo.");

            return Result.Failure(errors);
        }

        var userSalt = _authService.GenerateSalt();


        var role = await _context.Role.FirstAsync(x => x.Name == "admin", cancellationToken);

        var user = new User
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Salt = userSalt,
            Password = _authService.HashPassword(request.Password, userSalt),
            AddressId = null,
            Created = DateTime.UtcNow,
            LastModified = DateTime.UtcNow,
            RoleId = role.Id
        };

        await _context.User.AddAsync(user, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
