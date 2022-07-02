using FastDrink.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace FastDrink.Domain.Entities;

public class User : AuditableEntity
{
    public int Id { get; set; }

    [Required, EmailAddress, MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required, MaxLength(40)]
    public string FirstName { get; set; } = string.Empty;

    [Required, MaxLength(40)]
    public string LastName { get; set; } = string.Empty;

    [Required, MaxLength(100)]
    public string Password { get; set; } = string.Empty;

    [Required]
    public byte[] Salt { get; set; } = Array.Empty<byte>();

    [Required]
    public int RoleId { get; set; }

    public Role? Role { get; set; }

    public int? AddressId { get; set; }

    public Address? Address { get; set; }
}
