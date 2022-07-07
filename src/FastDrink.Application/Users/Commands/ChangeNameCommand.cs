using FastDrink.Application.Common.Interfaces;
using FastDrink.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FastDrink.Application.Users.Commands;

public class ChangeNameCommand : IRequest<Result>
{
    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public string Password { get; set; } = "";

    public int UserId { get; set; }
}

public class ChangeNameCommandHandler : IRequestHandler<ChangeNameCommand, Result>
{
    private readonly IApplicationDbContext _context;
    private readonly IAuthService _authService;

    public ChangeNameCommandHandler(IApplicationDbContext context, IAuthService authService)
    {
        _context = context;
        _authService = authService;
    }

    public async Task<Result> Handle(ChangeNameCommand request, CancellationToken cancellationToken)
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

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

