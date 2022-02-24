﻿using FastDrink.Application.Common.Models;
using FastDrink.Application.Products.Commands.CreateProduct;
using FastDrink.Application.Products.Commands.DeleteProduct;
using FastDrink.Application.Products.Commands.UpdateProduct;
using FastDrink.Application.Products.DTOs;
using FastDrink.Application.Products.Queries;
using FastDrink.Application.Products.Queries.GetAllProducts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastDrink.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<ProductDto>>> GetAll([FromQuery] GetAllProductsCustomerQuery query)
    {
        return await _mediator.Send(query);
    }

    [HttpGet("admin")]
    [Authorize(Policy = "MustBeAdmin")]
    public async Task<ActionResult<PaginatedList<ProductDto>>> GetAllAdmin([FromQuery] GetAllProductsAdminQuery query)
    {
        return await _mediator.Send(query);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetByIdProduct(int id)
    {
        var result = await _mediator.Send(new GetByIdProductQuery
        {
            Id = id
        });

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok(result.Product);
    }

    [HttpPost]
    [Authorize(Policy = "MustBeAdmin")]
    public async Task<ActionResult<CreateProductResult>> CreateProduct([FromForm] CreateProductRequest request)
    {
        var user = Domain.Entities.User.FromIdentity(User);

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
    public async Task<ActionResult<string[]>> DeleteProduct(int id)
    {
        var result = await _mediator.Send(new DeleteProductCommand
        {
            Id = id
        });

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return NoContent();
    }
}

