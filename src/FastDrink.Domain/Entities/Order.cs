using FastDrink.Domain.Common;
using FastDrink.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FastDrink.Domain.Entities;

public class Order : AuditableEntity
{
    public int Id { get; set; }

    [Required]
    public int UserId { get; set; }

    public User? User { get; set; }

    [Required]
    public float TotalPrice { get; set; }

    [Required]
    public int AddressId { get; set; }

    public Address? Address { get; set; }

    public ICollection<OrderProduct>? Products { get; set; }

    [NotMapped]
    [JsonIgnore]
    public virtual string OrderStatusString
    {
        get { return OrderStatus.ToString(); }
        set
        {
            if (Enum.TryParse(value, out OrderStatus orderStatus))
            {
                OrderStatus = orderStatus;
            }
        }
    }

    [Required, MaxLength(30)]
    public virtual OrderStatus OrderStatus { get; set; }
}
