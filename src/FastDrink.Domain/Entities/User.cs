using FastDrink.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace FastDrink.Domain.Entities;

public class User : AuditableEntity
{
    public int Id { get; set; }

    [Required, EmailAddress, MaxLength(150)]
    public string Email { get; set; }

    [Required, MaxLength(40)]
    public string FirstName { get; set; }

    [Required, MaxLength(40)]
    public string LastName { get; set; }

    [Required, MaxLength(100)]
    public string Password { get; set; }

    [Required]
    public byte[] Salt { get; set; } = Array.Empty<byte>();

    [Required]
    public int RoleId { get; set; }

    public Role? Role { get; set; }

    public int? AddressId { get; set; }

    public Address? Address { get; set; }

    public static User FromIdentity(ClaimsPrincipal p)
    {
        return new User
        {
            Email = p.Claims.Single(c => c.Type == ClaimTypes.Email).Value,
            Role = new Role
            {
                Name = p.Claims.Single(c => c.Type == ClaimTypes.Role).Value,
            },
        };
    }
}
