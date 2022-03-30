using FastDrink.Application.Common.Models;
using FastDrink.Application.Products.Commands.CreateProduct;
using FastDrink.Application.Products.Commands.DeleteProduct;
using FastDrink.Application.Products.Commands.RevocerProduct;
using FastDrink.Application.Products.Commands.UpdateProduct;
using FastDrink.Application.Products.DTOs;
using FastDrink.Application.Products.Queries;
using FastDrink.Application.Products.Queries.GetAllProducts;
using FastDrink.Application.Users.DTOs;
using HashidsNet;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastDrink.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHashids _hashids;

    public ProductController(IMediator mediator, IHashids hashids)
    {
        _mediator = mediator;
        _hashids = hashids;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<ProductDto>>> GetAll([FromQuery] GetAllProductsCustomerQuery query)
    {
        var result = await _mediator.Send(query);
        if (result == null || result.Items == null)
        {
            return BadRequest();
        }

        result.Items = result.Items.Select(x =>
        {
            x.Id = _hashids.Encode(int.Parse(x.Id));
            return x;
        }).ToList();

        return result;
    }

    [HttpGet("admin")]
    [Authorize(Policy = "MustBeAdmin")]
    public async Task<ActionResult<PaginatedList<ProductDto>>> GetAllAdmin([FromQuery] GetAllProductsAdminQuery query)
    {
        var result = await _mediator.Send(query);
        if (result == null || result.Items == null)
        {
            return BadRequest();
        }

        result.Items = result.Items.Select(x =>
        {
            x.Id = _hashids.Encode(int.Parse(x.Id));
            return x;
        }).ToList();

        return result;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetByIdProduct(string id)
    {
        var result = await _mediator.Send(new GetByIdProductQuery
        {
            Id = _hashids.Decode(id)[0],
        });

        if (result.Succeeded == false || result.Product == null)
        {
            return BadRequest(result.Errors);
        }

        result.Product.Id = _hashids.Encode(int.Parse(result.Product.Id));

        return Ok(result.Product);
    }

    [HttpPost]
    [Authorize(Policy = "MustBeAdmin")]
    public async Task<ActionResult<CreateProductResult>> CreateProduct([FromForm] CreateProductRequest request)
    {
        var user = UserClaims.GetUser(User);

        var product = new CreateProductCommand
        {
            Product = request,
            EmailCreator = user.Email,
        };
        var result = await _mediator.Send(product);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok(result);
    }

    [HttpPut]
    [Authorize(Policy = "MustBeAdmin")]
    public async Task<ActionResult<string[]>> UpdateProduct([FromBody] UpdateProductCommand request)
    {
        var result = await _mediator.Send(request);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "MustBeAdmin")]
    /// This method set a date in DeleteAt in Product.
    public async Task<ActionResult<string[]>> SoftDeleteProduct(string id)
    {
        var result = await _mediator.Send(new DeleteProductCommand
        {
            Id = _hashids.Decode(id)[0],
        });

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return NoContent();
    }

    [HttpDelete("hard-delete/{id}")]
    [Authorize(Policy = "MustBeAdmin")]
    /// This method remove the Product from the database.
    public async Task<ActionResult> HardDeleteProduct(string id)
    {
        var result = await _mediator.Send(new HardDeleteProductCommand
        {
            Id = _hashids.Decode(id)[0],
        });

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return NoContent();
    }

    [HttpPut("recover-product/{id}")]
    [Authorize(Policy = "MustBeAdmin")]
    /// Only recover products that hasn't be hard delete.
    public async Task<ActionResult> RecoverProduct(string id)
    {
        var result = await _mediator.Send(new RecoverProductCommand
        {
            Id = _hashids.Decode(id)[0],
        });

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return NoContent();
    }
}

