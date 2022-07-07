using FastDrink.Application.Common.Interfaces;
using FastDrink.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FastDrink.Application.Users.Commands;

public class ChangePasswordCommand : IRequest<Result>
{
    public int UserId { get; set; }
    public string Password { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IAuthService _authService;

    public ChangePasswordCommandHandler(IAuthService authService, IApplicationDbContext context)
    {
        _authService = authService;
        _context = context;
    }

    public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.User.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);

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

        user.Password = _authService.HashPassword(request.NewPassword, user.Salt);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
