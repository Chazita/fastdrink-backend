﻿using FastDrink.Application.Addresses.DTOs;
using FastDrink.Application.Common.Mappings;
using FastDrink.Domain.Entities;

namespace FastDrink.Application.Orders.DTOs;

public class OrderDto : IMapFrom<Order>
{
    public string Id { get; set; } = "";

    public float TotalPrice { get; set; }

    public AddressDto Address { get; set; } = null!;

    public IList<OrderProductDto> Products { get; set; } = null!;
}
