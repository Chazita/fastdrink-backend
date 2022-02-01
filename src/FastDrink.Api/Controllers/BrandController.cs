using FastDrink.Application.BaseTypes.Commands;
using FastDrink.Application.BaseTypes.Queries;
using FastDrink.Domain.Common;
using FastDrink.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastDrink.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BrandController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IList<BaseType>>> GetAll()
        {
            var brands = await _mediator.Send(new GetAllBaseTypeQuery<Brand>());

            return Ok(brands);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var query = new GetByIdBaseTypeQuery<Brand>
            {
                Id = id
            };

            var category = await _mediator.Send(query);

            if (category == null)
            {
                return BadRequest($"No exist {typeof(Brand).Name} with {id} id.");
            }

            return Ok(category);
        }

        [HttpPost]
        [Authorize(Policy = "MustBeAdmin")]
        public async Task<ActionResult> CreateBrand([FromBody] CreateBaseTypeCommand<Brand> command)
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
        public async Task<ActionResult> UpdateBrand([FromBody] UpdateBaseTypeCommand<Brand> command)
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
        public async Task<ActionResult> DeleteBrand(int id)
        {
            var category = new DeleteBaseTypeCommand<Brand>
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
}
