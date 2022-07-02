using FastDrink.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace FastDrink.Domain.Entities;

public class FlavorDetails : BaseDetails
{
    [MaxLength(50)]
    public string Flavor { get; set; } = string.Empty;
}
