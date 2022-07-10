using FastDrink.Application.Orders.Commands;
using FastDrink.Application.Orders.DTOs;
using FastDrink.Application.Orders.Queries;
using FastDrink.Application.Users.DTOs;
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

        var address = await _mediator.Send(request.Address);

        if (!address.Succeeded)
        {
            return BadRequest();
        }

        var result = await _mediator.Send(
            new CreateOrderCommand
            {
                AddressId = address.AddressId,
                UserId = userId,
                OrderProducts = request.OrderProducts
            });

        return Ok(result);
    }

    [HttpPut("modify-status")]
    [Authorize(Policy = "MustBeAdmin")]
    public async Task<ActionResult> ModifyStatus([FromBody] UpdateOrderStatusCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(result);
        }

        return NoContent();
    }

    [HttpDelete("modify-status")]
    [Authorize(Policy = "MustBeUser")]
    public async Task<ActionResult> DeleteOrder([FromBody] CancelOrderCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(result);
        }

        return NoContent();
    }

    [HttpGet("my-orders")]
    [Authorize(Policy = "MustBeUser")]
    public async Task<ActionResult> GetMyOrders([FromQuery] int? PageNumber)
    {
        var userId = _hashids.Decode(UserClaims.GetUser(User).Id)[0];

        var orders = await _mediator.Send(new GetMyOrdersQuery
        {
            UserId = userId,
            PageNumber = PageNumber == null ? 1 : (int)PageNumber
        });

        foreach (var order in orders.Items)
        {
            order.Id = _hashids.Encode(int.Parse(order.Id));
            order.Address.Id = _hashids.Encode(int.Parse(order.Address.Id));
        }

        return Ok(orders);
    }

    [HttpGet("orders")]
    [Authorize(Policy = "MustBeAdmin")]
    public async Task<ActionResult> GetOrders([FromQuery] GetOrdersQuery query)
    {
        var orders = await _mediator.Send(query);

        foreach (var order in orders.Items)
        {
            order.Id = _hashids.Encode(int.Parse(order.Id));
            order.User.Id = _hashids.Encode(int.Parse(order.User.Id));
            order.Address.Id = _hashids.Encode(int.Parse(order.Address.Id));
        }

        return Ok(orders);
    }
}
