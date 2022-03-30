using FastDrink.Application.Orders.Commands;
using FastDrink.Application.Orders.DTOs;
using FastDrink.Application.Users.DTOs;
using FastDrink.Application.Users.Queries.GetIdAddress;
using HashidsNet;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastDrink.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHashids _hashids;

    public OrderController(IMediator mediator, IHashids hashids)
    {
        _mediator = mediator;
        _hashids = hashids;
    }

    [HttpPost]
    [Authorize(Policy = "MustBeUser")]
    public async Task<ActionResult> CreateOrder(CreateOrderRequest request)
    {
        var user = UserClaims.GetUser(User);
        var userId = _hashids.Decode(user.Id)[0];
        int addressId;

        if (request.Address == null)
        {
            var address = await _mediator.Send(new GetIdAddressQuery
            {
                UserId = userId
            });

            if (!address.Succeeded || address.Address == null)
            {
                return BadRequest(address.Errors);
            }

            addressId = address.Address.Id;
        }
        else
        {
            var address = await _mediator.Send(request.Address);

            if (!address.Succeeded)
            {
                return BadRequest();
            }

            addressId = address.AddressId;
        }

        var result = await _mediator.Send(new CreateOrderCommand
        {
            AddressId = addressId,
            UserId = userId,
            OrderProducts = request.OrderProducts
        });

        return Ok(result);
    }
}
