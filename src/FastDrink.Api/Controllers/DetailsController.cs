using FastDrink.Application.Common.Models;
using FastDrink.Application.Products.Commands;
using FastDrink.Application.Products.DTOs.Details;
using FastDrink.Application.Products.Queries;
using FastDrink.Domain.Entities;
using HashidsNet;
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
    private readonly IHashids _hashids;

    public DetailsController(IMediator mediator, IHashids hashids)
    {
        _mediator = mediator;
        _hashids = hashids;
    }

    #region Create Details
    [HttpPost("create-details-alcohol")]
    public async Task<ActionResult> CreateDetailsAlcohol(AlcoholDetailsDto alcoholDetails)
    {
        var id = _hashids.Decode(alcoholDetails.ProductId)[0];

        var result = await _mediator.Send(new CreateDetailsCommand<AlcoholDetails>
        {
            Entity = new()
            {
                AlcoholContent = alcoholDetails.AlcoholContent,
                ProductId = id,
            },
        });

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return NoContent();
    }

    [HttpPost("create-details-beer")]
    public async Task<ActionResult> CreateDetailsBeer(BeerDetailsDto beerDetails)
    {
        var id = _hashids.Decode(beerDetails.ProductId)[0];

        var result = await _mediator.Send(new CreateDetailsCommand<BeerDetails>
        {
            Entity = new()
            {
                AlcoholContent = beerDetails.AlcoholContent,
                ProductId = id,
                Style = beerDetails.Style,
            },
        });

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return NoContent();
    }

    [HttpPost("create-details-energy-drink")]
    public async Task<ActionResult> CreateDetailsEnergyDrink(EnergyDrinkDetailsDto energyDrinkDetails)
    {
        var id = _hashids.Decode(energyDrinkDetails.ProductId)[0];

        var result = await _mediator.Send(new CreateDetailsCommand<EnergyDrinkDetails>
        {
            Entity = new()
            {
                Dietetics = energyDrinkDetails.Dietetics,
                ProductId = id,
                Energizing = energyDrinkDetails.Energizing,
                Flavor = energyDrinkDetails.Flavor,
                NonAlcoholic = energyDrinkDetails.NonAlcoholic
            },
        });

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return NoContent();
    }

    [HttpPost("create-details-flavor")]
    public async Task<ActionResult> CreateDetailsEnergyDrink(FlavorDetailsDto flavorDetails)
    {
        var id = _hashids.Decode(flavorDetails.ProductId)[0];

        var result = await _mediator.Send(new CreateDetailsCommand<FlavorDetails>
        {
            Entity = new()
            {
                Flavor = flavorDetails.Flavor,
                ProductId = id
            },
        });

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return NoContent();
    }

    [HttpPost("create-details-soda")]
    public async Task<ActionResult> CreateDetailsEnergyDrink(SodaDetailsDto sodaDetails)
    {
        var id = _hashids.Decode(sodaDetails.ProductId)[0];

        var result = await _mediator.Send(new CreateDetailsCommand<SodaDetails>
        {
            Entity = new()
            {
                Dietetics = sodaDetails.Dietetics,
                ProductId = id,
                Flavor = sodaDetails.Flavor
            },
        });

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return NoContent();
    }

    [HttpPost("create-details-water")]
    public async Task<ActionResult> CreateDetailsEnergyDrink(WaterDetailsDto waterDetails)
    {
        var id = _hashids.Decode(waterDetails.ProductId)[0];

        var result = await _mediator.Send(new CreateDetailsCommand<WaterDetails>
        {
            Entity = new()
            {
                Gasified = waterDetails.Gasified,
                ProductId = id,
                LowInSodium = waterDetails.LowInSodium
            },
        });

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return NoContent();
    }

    [HttpPost("create-details-wine")]
    public async Task<ActionResult> CreateDetailsEnergyDrink(WineDetailsDto wineDetails)
    {
        var id = _hashids.Decode(wineDetails.ProductId)[0];

        var result = await _mediator.Send(new CreateDetailsCommand<WineDetails>
        {
            Entity = new()
            {
                AlcoholContent = wineDetails.AlcoholContent,
                ProductId = id,
                Style = wineDetails.Style,
                Variety = wineDetails.Variety
            },
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
    public async Task<ActionResult> UpdateDetailsAlcohol(AlcoholDetailsDto alcoholDetails)
    {
        var id = _hashids.Decode(alcoholDetails.ProductId)[0];

        var result = await _mediator.Send(new UpdateDetailsCommand<AlcoholDetails>
        {
            Entity = new()
            {
                AlcoholContent = alcoholDetails.AlcoholContent,
                ProductId = id
            },
        });

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return NoContent();
    }

    [HttpPut("update-details-beer")]
    public async Task<ActionResult> UpdateDetailsBeer(BeerDetailsDto beerDetails)
    {
        var id = _hashids.Decode(beerDetails.ProductId)[0];

        var result = await _mediator.Send(new UpdateDetailsCommand<BeerDetails>
        {
            Entity = new()
            {
                AlcoholContent = beerDetails.AlcoholContent,
                ProductId = id,
                Style = beerDetails.Style,
            },
        });

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return NoContent();
    }

    [HttpPut("update-details-energy-drink")]
    public async Task<ActionResult> UpdateDetailsEnergyDrink(EnergyDrinkDetailsDto energyDrinkDetails)
    {
        var id = _hashids.Decode(energyDrinkDetails.ProductId)[0];

        var result = await _mediator.Send(new UpdateDetailsCommand<EnergyDrinkDetails>
        {
            Entity = new()
            {
                Dietetics = energyDrinkDetails.Dietetics,
                ProductId = id,
                Energizing = energyDrinkDetails.Energizing,
                Flavor = energyDrinkDetails.Flavor,
                NonAlcoholic = energyDrinkDetails.NonAlcoholic
            },
        });

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return NoContent();
    }

    [HttpPut("update-details-flavor")]
    public async Task<ActionResult> UpdateDetailsEnergyDrink(FlavorDetailsDto flavorDetails)
    {
        var id = _hashids.Decode(flavorDetails.ProductId)[0];

        var result = await _mediator.Send(new UpdateDetailsCommand<FlavorDetails>
        {
            Entity = new()
            {
                Flavor = flavorDetails.Flavor,
                ProductId = id
            },
        });

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return NoContent();
    }

    [HttpPut("update-details-soda")]
    public async Task<ActionResult> UpdateDetailsEnergyDrink(SodaDetailsDto sodaDetails)
    {
        var id = _hashids.Decode(sodaDetails.ProductId)[0];

        var result = await _mediator.Send(new UpdateDetailsCommand<SodaDetails>
        {
            Entity = new()
            {
                Dietetics = sodaDetails.Dietetics,
                ProductId = id,
                Flavor = sodaDetails.Flavor
            },
        });

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return NoContent();
    }

    [HttpPut("update-details-water")]
    public async Task<ActionResult> UpdateDetailsEnergyDrink(WaterDetailsDto waterDetails)
    {
        var id = _hashids.Decode(waterDetails.ProductId)[0];

        var result = await _mediator.Send(new UpdateDetailsCommand<WaterDetails>
        {
            Entity = new()
            {
                Gasified = waterDetails.Gasified,
                ProductId = id,
                LowInSodium = waterDetails.LowInSodium
            },
        });

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return NoContent();
    }

    [HttpPut("update-details-wine")]
    public async Task<ActionResult> UpdateDetailsEnergyDrink(WineDetailsDto windDetails)
    {
        var id = _hashids.Decode(windDetails.ProductId)[0];

        var result = await _mediator.Send(new UpdateDetailsCommand<WineDetails>
        {
            Entity = new()
            {
                AlcoholContent = windDetails.AlcoholContent,
                ProductId = id,
                Style = windDetails.Style,
                Variety = windDetails.Variety
            },
        });

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return NoContent();
    }
    #endregion

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteDetails(string id)
    {
        var numberId = _hashids.Decode(id)[0];
        var product = await _mediator.Send(new GetByIdProductQuery
        {
            Id = numberId
        });

        Result? result = null;

        switch (product.Product?.Category?.Name)
        {
            case "cerveza":
                result = await _mediator.Send(new DeleteDetailsCommand<BeerDetails>
                {
                    Id = numberId
                });
                break;

            case "alcohol":
                result = await _mediator.Send(new DeleteDetailsCommand<AlcoholDetails>
                {
                    Id = numberId
                });
                break;

            case "bebida_energizante":
                result = await _mediator.Send(new DeleteDetailsCommand<EnergyDrinkDetails>
                {
                    Id = numberId
                });
                break;

            case "gaseosa":
                result = await _mediator.Send(new DeleteDetailsCommand<SodaDetails>
                {
                    Id = numberId
                });
                break;

            case "agua":
                result = await _mediator.Send(new DeleteDetailsCommand<WaterDetails>
                {
                    Id = numberId
                });
                break;

            case "vino":
                result = await _mediator.Send(new DeleteDetailsCommand<WineDetails>
                {
                    Id = numberId
                });
                break;

            case "jugo":
                result = await _mediator.Send(new DeleteDetailsCommand<FlavorDetails>
                {
                    Id = numberId
                });
                break;

            case "bebida_isotónica":
                result = await _mediator.Send(new DeleteDetailsCommand<FlavorDetails>
                {
                    Id = numberId
                });
                break;

            default:
                Dictionary<string, string> errors = new();
                errors.Add("Detalles", "Dicha categoria no esta definida.");
                result = Result.Failure(errors);
                break;
        }

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }


        return NoContent();
    }

}
