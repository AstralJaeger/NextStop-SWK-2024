using Microsoft.AspNetCore.Mvc;
using NextStop.Api.DTOs;
using NextStop.Api.Mappers;
using NextStop.Service.Interfaces;

namespace NextStop.Api.Controllers;

[ApiController]
//[ProducesResponseType(StatusCodes.Status200OK)] 
[Route("api/[controller]")]

public class RouteController : ControllerBase
{
    private readonly IRouteService routeService;

    public RouteController(IRouteService routeService)
    {
        this.routeService = routeService ?? throw new ArgumentNullException(nameof(routeService));
    }

    [HttpGet]
    public async Task<ActionResult> GetAllRoutes()
    {
        var result = await routeService.GetAllRoutesAsync();
        return Ok(result.Select(r => r.ToRouteDto()));
    }

    [HttpGet("by-id/{id:int}")]
    public async Task<ActionResult> GetRouteById(int id)
    {
        var result = await routeService.GetRouteByIdAsync(id);
        if (result is null)
        {
            return NotFound(StatusInfo.InvalidRouteId(id));
        }
        return Ok(result.ToRouteDto());
    }

    [HttpGet("by-name/{name}")]
    public async Task<ActionResult> GetRouteByName(string name)
    {
        var result = await routeService.GetRouteByNameAsync(name);
        if (result is null)
        {
            return NotFound(StatusInfo.InvalidRouteName(name));
        }

        return Ok(result.ToRouteDto());
    }


    [HttpGet("by-validFrom/{validFrom}")]
    public async Task<ActionResult> GetRoutesByValidFrom(string validFrom)
    {
        if (!DateTime.TryParse(validFrom, out var validFromDate))
        {
            return BadRequest("Invalid date format for validFrom.");
        }

        var result = await routeService.GetRoutesByValidFromAsync(validFromDate);
        if (result is null)
        {
            return NotFound(StatusInfo.InfalidValidFromForRoute(validFrom));
        }
        return Ok(result);
    }

    [HttpGet("by-validTo/{validTo}")]
    public async Task<ActionResult> GetRoutesByValidTo(string validTo)
    {
        if (!DateTime.TryParse(validTo, out var validToDate))
        {
            return BadRequest("Invalid date format for validTo.");
        }

        var result = await routeService.GetRoutesByValidToAsync(validToDate);
        if (result is null)
        {
            return NotFound(StatusInfo.InfalidValidToForRoute(validTo));
        }
        return Ok(result);
    }
    
    [HttpPost]
    [Produces("application/json", "text/plain")]
    public async Task<ActionResult> CreateRoute(RouteForCreationDto routeDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (routeDto.Id != 0 && await routeService.RouteAlreadyExist(routeDto.Id))
        {
            return Conflict(StatusInfo.RouteAlreadyExists(routeDto.Id));
        }
            
        var newRoute = routeDto.ToRoute();
        await routeService.InsertRouteAsync(newRoute);
        return CreatedAtAction(
            actionName: nameof(GetRouteById), 
            routeValues: new { id = newRoute.Id }, 
            value: newRoute.ToRouteDto());
    }

}