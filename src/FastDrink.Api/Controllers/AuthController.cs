using FastDrink.Application.Auth.Commands.CreateAdmin;
using FastDrink.Application.Auth.Commands.CreateCustomer;
using FastDrink.Application.Auth.Commands.Login;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastDrink.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IConfiguration _configuration;
    public AuthController(IMediator mediator, IConfiguration configuration)
    {
        _mediator = mediator;
        _configuration = configuration;
    }

    [HttpPost("register-admin")]
    [Authorize(Policy = "MustBeAdmin")]
    public async Task<ActionResult> CreateAdmin(CreateAdminCommand request)
    {
        var result = await _mediator.Send(request);

        if (!result.Succeeded)
        {
            return BadRequest(result);
        }

        return NoContent();
    }

    [HttpPost("register-customer")]
    public async Task<ActionResult> CreateCustomer(CreateCustomerCommand request)
    {
        var result = await _mediator.Send(request);

        if (!result.Succeeded)
        {
            return BadRequest(result);
        }

        return NoContent();
    }

    [HttpGet("check")]
    [Authorize(Policy = "MustBeUser")]
    public ActionResult Get()
    {
        var user = FastDrink.Domain.Entities.User.FromIdentity(User);
        return Ok(user);
    }

    [HttpGet("log-out")]
    public ActionResult LogOut()
    {
        HttpContext.Response.Cookies.Delete(_configuration["CookieName"]);

        return NoContent();
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResult>> Login(LoginCommand request)
    {
        var result = await _mediator.Send(request);

        if (!result.Succeeded || result.Token == null || result == null)
        {
            return BadRequest(result);
        }

        HttpContext.Response.Cookies.Append(_configuration["CookieName"],
                                            result.Token,
                                            new CookieOptions { HttpOnly = true });
        return NoContent();
    }
}
