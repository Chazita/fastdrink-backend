using FastDrink.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace FastDrink.Domain.Entities;

public class SodaDetails : BaseDetails
{
    [MaxLength(50)]
    public string Flavor { get; set; } = string.Empty;

    public bool Dietetics { get; set; }
}
