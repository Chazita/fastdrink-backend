using System.ComponentModel.DataAnnotations;

namespace FastDrink.Domain.Enums;

public enum OrderStatus
{
    [Display(Name = "Pending")]
    Pending,    
    [Display(Name = "Shipped")]
    Shipped,
    [Display(Name = "Completed")]
    Completed,
    [Display(Name = "Canceled")]
    Canceled,
    [Display(Name = "Declined")]
    Declined,

}