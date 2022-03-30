using System.Security.Claims;

namespace FastDrink.Application.Users.DTOs;

public class UserClaims
{
    public string Id { get; set; } = "";
    public string Email { get; set; } = "";
    public string Role { get; set; } = "";

    public static UserClaims GetUser(ClaimsPrincipal p)
    {

        return new UserClaims
        {
            Id = p.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value,
            Email = p.Claims.Single(c => c.Type == ClaimTypes.Email).Value,
            Role = p.Claims.Single(c => c.Type == ClaimTypes.Role).Value,
        };

    }
}
