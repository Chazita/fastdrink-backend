using FastDrink.Application.Addresses.Commands;
using FastDrink.Application.Users.Commands;
using FastDrink.Application.Users.DTOs;
using HashidsNet;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastDrink.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHashids _hashids;

    public UserController(IMediator mediator, IHashids hashids)
    {
        _mediator = mediator;
        _hashids = hashids;
    }

    [HttpPost("add-prefered-address")]
    [Authorize(Policy = "MustBeUser")]
    public async Task<ActionResult> AddPreferedAddress(CreateAddressCommand request)
    {
        var userData = UserClaims.GetUser(User);

        var addressResult = await _mediator.Send(request);

        if (!addressResult.Succeeded)
        {
            return BadRequest(addressResult.Errors);
        }

        var result = await _mediator.Send(new AddPreferedAddressCommand
        {
            AddressId = addressResult.AddressId,
            UserId = _hashids.Decode(userData.Id)[0]
        });

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return NoContent();
    }
}
