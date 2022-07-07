using FastDrink.Application.Common.Interfaces;
using FastDrink.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FastDrink.Application.Users.Commands;

public class ChangeEmailCommand : IRequest<Result>
{
    public string NewEmail { get; set; } = "";

    public int UserId { get; set; }

    public string Password { get; set; } = "";
}

public class ChangeEmailCommandHandler : IRequestHandler<ChangeEmailCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IAuthService _authService;

    public ChangeEmailCommandHandler(IApplicationDbContext context, IAuthService authService)
    {
        _context = context;
        _authService = authService;
    }

    public async Task<Result> Handle(ChangeEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.User.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
        var userEmail = await _context.User.FirstOrDefaultAsync(x => x.Email == request.NewEmail, cancellationToken);

        if (userEmail != null)
        {
            return Result.Failure(new Dictionary<string, string>
            {
                { "Correo", "El nuevo correo ya esta registrado." }
            });
        }

        if (user == null)
        {
            return Result.Failure(new Dictionary<string, string>
            {
                { "Usuario", "Usuario no encontrado." }
            });
        }

        if (user.Password != _authService.HashPassword(request.Password, user.Salt))
        {
            return Result.Failure(new Dictionary<string, string>
            {
                { "Contraseña", "Las contraseñas no coinciden." }
            });
        }

        user.Email = request.NewEmail;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
