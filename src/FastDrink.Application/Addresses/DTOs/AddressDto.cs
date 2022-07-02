using FastDrink.Application.Common.Mappings;
using FastDrink.Domain.Entities;

namespace FastDrink.Application.Addresses.DTOs;

public class AddressDto : IMapFrom<Address>
{
    public string Id { get; set; } = "";

    public string Province { get; set; } = "";

    public string City { get; set; } = "";

    public string Street { get; set; } = "";

    public string Code { get; set; } = "";

}
