using FastDrink.Application.BaseTypes.Commands;
using FastDrink.Application.BaseTypes.Queries;
using FastDrink.Domain.Common;
using FastDrink.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastDrink.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContainerController : ControllerBase
{
    private readonly IMediator _mediator;

    public ContainerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IList<BaseType>>> GetAll()
    {
        var categories = await _mediator.Send(new GetAllBaseTypeQuery<Container>());
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        var query = new GetByIdBaseTypeQuery<Container>
        {
            Id = id
        };

        var category = await _mediator.Send(query);

        if (category == null)
        {
            return BadRequest($"No exist {typeof(Container).Name} with {id} id.");
        }

        return Ok(category);
    }

    [HttpPost]
    [Authorize(Policy = "MustBeAdmin")]
    public async Task<ActionResult> CreateContainer([FromBody] CreateBaseTypeCommand<Container> command)
    {
        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(result);
        }

        return NoContent();
    }

    [HttpPut]
    [Authorize(Policy = "MustBeAdmin")]
    public async Task<ActionResult> UpdateContainer([FromBody] UpdateBaseTypeCommand<Container> command)
    {
        var result = await _mediator.Send(command);

        if (!result.Succeeded)
        {
            return BadRequest(result);
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "MustBeAdmin")]
    public async Task<ActionResult> DeleteContainer(int id)
    {
        var category = new DeleteBaseTypeCommand<Container>
        {
            Id = id
        };

        var result = await _mediator.Send(category);

        if (!result.Succeeded)
        {
            return BadRequest(result);
        }

        return NoContent();
    }
}
