using FastDrink.Application.Common.Interfaces;
using FastDrink.Application.Common.Models;
using FastDrink.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FastDrink.Application.Auth.Commands.CreateAdmin;

public class CreateAdminCommand : IRequest<Result>
{
    public string Email { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Password { get; set; }
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
            List<string> errors = new();

            return Result.Failure(new[]
            {
                "Email already register"
            });
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
            Created = DateTime.Now,
            LastModified = DateTime.Now,
            RoleId = role.Id
        };

        await _context.User.AddAsync(user, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
