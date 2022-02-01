using FastDrink.Application.Common.Models;
using FastDrink.Application.Products.Commands;
using FastDrink.Application.Products.Queries;
using FastDrink.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FastDrink.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Policy = "MustBeAdmin")]
public class DetailsController : ControllerBase
{

    private readonly IMediator _mediator;

    public DetailsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    #region Create Details
    [HttpPost("create-details-alcohol")]
    public async Task<ActionResult> CreateDetailsAlcohol(AlcoholDetails alcoholDetails)
    {

        var result = await _mediator.Send(new CreateDetailsCommand<AlcoholDetails>
        {
            Entity = alcoholDetails,
        });

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return NoContent();
    }

    [HttpPost("create-details-beer")]
    public async Task<ActionResult> CreateDetailsBeer(BeerDetails beerDetails)
    {

        var result = await _mediator.Send(new CreateDetailsCommand<BeerDetails>
        {
            Entity = beerDetails,
        });

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return NoContent();
    }

    [HttpPost("create-details-energy-drink")]
    public async Task<ActionResult> CreateDetailsEnergyDrink(EnergyDrinkDetails energyDrinkDetails)
    {

        var result = await _mediator.Send(new CreateDetailsCommand<EnergyDrinkDetails>
        {
            Entity = energyDrinkDetails,
        });

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return NoContent();
    }

    [HttpPost("create-details-flavor")]
    public async Task<ActionResult> CreateDetailsEnergyDrink(FlavorDetails flavorDetails)
    {

        var result = await _mediator.Send(new CreateDetailsCommand<FlavorDetails>
        {
            Entity = flavorDetails,
        });

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return NoContent();
    }

    [HttpPost("create-details-soda")]
    public async Task<ActionResult> CreateDetailsEnergyDrink(SodaDetails sodaDetails)
    {

        var result = await _mediator.Send(new CreateDetailsCommand<SodaDetails>
        {
            Entity = sodaDetails,
        });

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return NoContent();
    }

    [HttpPost("create-details-water")]
    public async Task<ActionResult> CreateDetailsEnergyDrink(WaterDetails waterDetails)
    {

        var result = await _mediator.Send(new CreateDetailsCommand<WaterDetails>
        {
            Entity = waterDetails,
        });

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return NoContent();
    }

    [HttpPost("create-details-wine")]
    public async Task<ActionResult> CreateDetailsEnergyDrink(WineDetails windDetails)
    {

        var result = await _mediator.Send(new CreateDetailsCommand<WineDetails>
        {
            Entity = windDetails,
        });

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return NoContent();
    }
    #endregion

    #region Update Details
    [HttpPut("update-details-alcohol")]
    public async Task<ActionResult> UpdateDetailsAlcohol(AlcoholDetails alcoholDetails)
    {

        var result = await _mediator.Send(new UpdateDetailsCommand<AlcoholDetails>
        {
            Entity = alcoholDetails,
        });

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return NoContent();
    }

    [HttpPut("update-details-beer")]
    public async Task<ActionResult> UpdateDetailsBeer(BeerDetails beerDetails)
    {

        var result = await _mediator.Send(new UpdateDetailsCommand<BeerDetails>
        {
            Entity = beerDetails,
        });

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return NoContent();
    }

    [HttpPut("update-details-energy-drink")]
    public async Task<ActionResult> UpdateDetailsEnergyDrink(EnergyDrinkDetails energyDrinkDetails)
    {

        var result = await _mediator.Send(new UpdateDetailsCommand<EnergyDrinkDetails>
        {
            Entity = energyDrinkDetails,
        });

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return NoContent();
    }

    [HttpPut("update-details-flavor")]
    public async Task<ActionResult> UpdateDetailsEnergyDrink(FlavorDetails flavorDetails)
    {

        var result = await _mediator.Send(new UpdateDetailsCommand<FlavorDetails>
        {
            Entity = flavorDetails,
        });

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return NoContent();
    }

    [HttpPut("update-details-soda")]
    public async Task<ActionResult> UpdateDetailsEnergyDrink(SodaDetails sodaDetails)
    {

        var result = await _mediator.Send(new UpdateDetailsCommand<SodaDetails>
        {
            Entity = sodaDetails,
        });

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return NoContent();
    }

    [HttpPut("update-details-water")]
    public async Task<ActionResult> UpdateDetailsEnergyDrink(WaterDetails waterDetails)
    {

        var result = await _mediator.Send(new UpdateDetailsCommand<WaterDetails>
        {
            Entity = waterDetails,
        });

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return NoContent();
    }

    [HttpPut("update-details-wine")]
    public async Task<ActionResult> UpdateDetailsEnergyDrink(WineDetails windDetails)
    {

        var result = await _mediator.Send(new UpdateDetailsCommand<WineDetails>
        {
            Entity = windDetails,
        });

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return NoContent();
    }
    #endregion

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteDetails(int id)
    {
        var product = await _mediator.Send(new GetByIdProductQuery
        {
            Id = id
        });

        Result? result = null;

        switch (product.Product?.Category?.Name)
        {
            case "cerveza":
                result = await _mediator.Send(new DeleteDetailsCommand<BeerDetails>
                {
                    Id = id
                });
                break;

            case "alcohol":
                result = await _mediator.Send(new DeleteDetailsCommand<AlcoholDetails>
                {
                    Id = id
                });
                break;

            case "bebida_energizante":
                result = await _mediator.Send(new DeleteDetailsCommand<EnergyDrinkDetails>
                {
                    Id = id
                });
                break;

            case "gaseosa":
                result = await _mediator.Send(new DeleteDetailsCommand<SodaDetails>
                {
                    Id = id
                });
                break;

            case "agua":
                result = await _mediator.Send(new DeleteDetailsCommand<WaterDetails>
                {
                    Id = id
                });
                break;

            case "vino":
                result = await _mediator.Send(new DeleteDetailsCommand<WineDetails>
                {
                    Id = id
                });
                break;

            case "jugo":
                result = await _mediator.Send(new DeleteDetailsCommand<FlavorDetails>
                {
                    Id = id
                });
                break;

            case "bebida_isotónica":
                result = await _mediator.Send(new DeleteDetailsCommand<FlavorDetails>
                {
                    Id = id
                });
                break;

            default:
                result = Result.Failure(new[]
                {
                    "Category doesn't exist"
                });
                break;
        }

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }


        return NoContent();
    }

}
