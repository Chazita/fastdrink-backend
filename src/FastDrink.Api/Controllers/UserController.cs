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

    [HttpPost("change-email")]
    [Authorize(Policy = "MustBeUser")]
    public async Task<ActionResult> ChangeEmail([FromBody] ChangeEmailRequest request)
    {
        var user = UserClaims.GetUser(User);

        var result = await _mediator.Send(new ChangeEmailCommand
        {
            NewEmail = request.NewEmail,
            UserId = _hashids.Decode(user.Id)[0],
            Password = request.Password,
        });

        if (!result.Succeeded)
        {
            return BadRequest(result);
        }

        return NoContent();
    }

    [HttpPost("change-name")]
    [Authorize(Policy = "MustBeUser")]
    public async Task<ActionResult> ChangeName([FromBody] ChangeNameRequest request)
    {
        var user = UserClaims.GetUser(User);

        var result = await _mediator.Send(new ChangeNameCommand
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Password = request.Password,
            UserId = _hashids.Decode(user.Id)[0]
        });

        if (!result.Succeeded)
        {
            return BadRequest(result);
        }

        return NoContent();
    }

    [HttpPost("change-password")]
    [Authorize(Policy = "MustBeUser")]
    public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        var user = UserClaims.GetUser(User);

        var result = await _mediator.Send(new ChangePasswordCommand
        {
            Password = request.Password,
            NewPassword = request.NewPassword,
            UserId = _hashids.Decode(user.Id)[0]
        });

        if (!result.Succeeded)
        {
            return BadRequest(result);
        }

        return NoContent();
    }
}
