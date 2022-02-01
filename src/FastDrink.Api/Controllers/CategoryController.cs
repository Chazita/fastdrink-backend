using FastDrink.Application.BaseTypes.Commands;
using FastDrink.Application.BaseTypes.Queries;
using FastDrink.Application.Common.Models;
using FastDrink.Domain.Common;
using FastDrink.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastDrink.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    public async Task<ActionResult<IList<BaseType>>> GetAll()
    {
        var categories = await _mediator.Send(new GetAllBaseTypeQuery<Category>());
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Category?>> GetById(int id)
    {
        var query = new GetByIdBaseTypeQuery<Category>
        {
            Id = id
        };

        var category = await _mediator.Send(query);

        if (category == null)
        {
            return BadRequest($"No exist {typeof(Category).Name} with {id} id.");
        }

        return Ok(category);
    }

    [HttpPost]
    [Authorize(Policy = "MustBeAdmin")]
    public async Task<ActionResult<Result>> CreateCategory([FromBody] CreateBaseTypeCommand<Category> command)
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
    public async Task<ActionResult<Result>> UpdateCategory([FromBody] UpdateBaseTypeCommand<Category> command)
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
    public async Task<ActionResult<Result>> DeleteCategory(int id)
    {
        var category = new DeleteBaseTypeCommand<Category>
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
