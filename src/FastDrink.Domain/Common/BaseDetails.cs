using System.ComponentModel.DataAnnotations;

namespace FastDrink.Domain.Common;

public class BaseDetails
{
    [Key]
    public int ProductId { get; set; }
}
