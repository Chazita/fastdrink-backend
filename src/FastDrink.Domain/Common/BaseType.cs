using System.ComponentModel.DataAnnotations;

namespace FastDrink.Domain.Common;

public class BaseType
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;
}