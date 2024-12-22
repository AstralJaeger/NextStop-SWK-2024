using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using NextStop.Api.DTOs;
using NextStop.Api.Mappers;
using NextStop.Service.Interfaces;

namespace NextStop.Api.Controllers;

/// <summary>
/// API Controller for managing routes.
/// Provides endpoints for creating, retrieving, and updating routes.
/// </summary>
[ApiController]
[Route("api/[controller]")]

public class RouteController : ControllerBase
{
    private readonly IRouteService routeService;

    //......................................................................

    /// <summary>
    /// Initializes a new instance of the <see cref="RouteController"/> class.
    /// </summary>
    /// <param name="routeService">The service to manage route operations.</param>
    public RouteController(IRouteService routeService)
    {
        this.routeService = routeService ?? throw new ArgumentNullException(nameof(routeService));
    }
    
    //**********************************************************************************
    // CREATE-Methods
    //**********************************************************************************

    /// <summary>
    /// Inserts a new route into the system.
    /// </summary>
    /// <param name="routeDto">The route data for creation.</param>
    /// <returns>The created route as a DTO.</returns>
    [HttpPost]
    [Produces("application/json", "text/plain")]
    public async Task<ActionResult<RouteDto>> InsertRoute(RouteForCreationDto routeDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (routeDto.Id is not 0 && await routeService.RouteAlreadyExist(routeDto.Id))
        {
            return Conflict(StatusInfo.RouteAlreadyExists(routeDto.Id));
        }
            
        var newRoute = routeDto.ToRoute();
        var generatedId = await routeService.InsertRouteAsync(newRoute);
        newRoute.Id = generatedId;
        
        return CreatedAtAction(
            actionName: nameof(GetRouteById), 
            routeValues: new { id = newRoute.Id }, 
            value: newRoute.ToRouteDto());
    }
    
    //**********************************************************************************
    //READ-Methods
    //**********************************************************************************

    /// <summary>
    /// Retrieves all routes.
    /// </summary>
    /// <returns>A collection of all routes as DTOs.</returns>
    [HttpGet]
    public async Task<ActionResult> GetAllRoutes()
    {
        var result = await routeService.GetAllRoutesAsync();
        return Ok(result.Select(r => r.ToRouteDto()));
    }

    //......................................................................

    /// <summary>
    /// Retrieves a route by its unique ID.
    /// </summary>
    /// <param name="id">The unique ID of the route.</param>
    /// <returns>The route as a DTO if found; otherwise, a 404 status.</returns>
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

    //......................................................................

    /// <summary>
    /// Retrieves a route by its unique name.
    /// </summary>
    /// <param name="name">The name of the route.</param>
    /// <returns>The route as a DTO if found; otherwise, a 404 status.</returns>
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

    //......................................................................

    /// <summary>
    /// Retrieves routes valid from a specific date.
    /// </summary>
    /// <param name="validFrom">The start date for validity.</param>
    /// <returns>A collection of routes valid from the specified date as DTOs.</returns>
    [HttpGet("by-validFrom/{validFrom}")]
    public async Task<ActionResult> GetRoutesByValidFrom(string validFrom)
    {
        if (!DateTime.TryParseExact(validFrom,
                "dd-MM-yyyy",
                new CultureInfo("de-AT"),
                DateTimeStyles.None,
                out var validFromDate))
        {
            return BadRequest("Invalid date format for validFrom.");
        }

        var result = await routeService.GetRoutesByValidFromAsync(validFromDate);
        if (!result.Any())
        {
            return NotFound(StatusInfo.InvalidValidFromForRoute(validFrom));
        }
        return Ok(result.Select(r => r.ToRouteDto()));
    }

    //......................................................................

    /// <summary>
    /// Retrieves routes valid until a specific date.
    /// </summary>
    /// <param name="validTo">The end date for validity.</param>
    /// <returns>A collection of routes valid until the specified date as DTOs.</returns>
    [HttpGet("by-validTo/{validTo}")]
    public async Task<ActionResult> GetRoutesByValidTo(string validTo)
    {
        if (!DateTime.TryParseExact(validTo,
                "dd-MM-yyyy",
                new CultureInfo("de-AT"),
                DateTimeStyles.None,
                out var validToDate))
        {
            return BadRequest("Invalid date format for validTo.");
        }

        var result = await routeService.GetRoutesByValidToAsync(validToDate);
        if (!result.Any())
        {
            return NotFound(StatusInfo.InvalidValidToForRoute(validTo));
        }
        return Ok(result.Select(r => r.ToRouteDto()));
    }
    


}