using AutoMapper;
using FastDrink.Application.Common.Interfaces;
using FastDrink.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FastDrink.Application.Auth.Commands.Login;

public class LoginCommand : IRequest<LoginResult>
{
    public string Email { get; set; } = "";
    public string Password { get; set; } = "";
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResult>
{
    private readonly IApplicationDbContext _context;
    private readonly IAuthService _authService;

    public LoginCommandHandler(IApplicationDbContext context, IAuthService authService, IMapper mapper)
    {
        _context = context;
        _authService = authService;
    }

    public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.User.Include(x => x.Role).FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

        if (user == null)
        {
            Dictionary<string, string> errors = new();

            errors.Add("Correo/Contraseña", "No coindicen correo/contraseña.");
            return LoginResult.Failure(errors);
        }

        var passwordMatch = user.Password == _authService.HashPassword(request.Password, user.Salt);

        if (!passwordMatch)
        {
            Dictionary<string, string> errors = new();

            errors.Add("Correo/Contraseña", "No coindicen correo/contraseña.");
            return LoginResult.Failure(errors);
        }

        return LoginResult.Success(_authService.GenerateToken(user));
    }
}

public class LoginResult : Result
{
    public LoginResult(bool succeeded, IDictionary<string, string> errors, string? token) : base(succeeded, errors)
    {
        Token = token;
    }

    public string? Token { get; set; }

    public static LoginResult Success(string token)
    {
        return new LoginResult(true, new Dictionary<string, string>(), token);
    }

    public static new LoginResult Failure(IDictionary<string, string> errors)
    {
        return new LoginResult(false, errors, null);
    }
}