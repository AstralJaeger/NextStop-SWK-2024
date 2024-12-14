﻿using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using NextStop.Api.DTOs;
using NextStop.Api.Mappers;
using NextStop.Service.Interfaces;

namespace NextStop.Api.Controllers;

/// <summary>
/// API Controller for managing Route Stop Points.
/// Provides endpoints for creating, retrieving, and validating route stop points.
/// </summary>
[ApiController]
[Route("api/[controller]")]

public class RouteStopPointController : ControllerBase
{
    private readonly IRouteStopPointService routeStopPointService;
    
    //......................................................................

    /// <summary>
    /// Initializes a new instance of the <see cref="RouteStopPointController"/> class.
    /// </summary>
    /// <param name="routeStopPointService">The service for managing route stop points.</param>
    public RouteStopPointController(IRouteStopPointService routeStopPointService)
    {
        this.routeStopPointService = routeStopPointService ?? throw new ArgumentNullException(nameof(routeStopPointService));
    }

    //**********************************************************************************
    // CREATE-Methods
    //**********************************************************************************

    /// <summary>
    /// Inserts a new route stop point into the system.
    /// </summary>
    /// <param name="routeStopPointDto">The data for creating a new route stop point.</param>
    /// <returns>The created route stop point.</returns>
    [HttpPost]
    [Produces("application/json", "text/plain")]
    public async Task<ActionResult> InsertRouteStopPoint(RouteStopPointForCreationDto routeStopPointDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (routeStopPointDto.Id is not 0 && await routeStopPointService.RouteStopPointAlreadyExists(routeStopPointDto.Id))
        {
            return Conflict(StatusInfo.RouteStopPointAlreadyExists(routeStopPointDto.Id));
        }
        
        var newRouteStopPoint = routeStopPointDto.ToRouteStopPoint();
        await routeStopPointService.InsertRouteStopPointAsync(newRouteStopPoint);

        return CreatedAtAction(
            actionName: nameof(GetRouteStopPointById), // Endpoint für die Rückgabe des Objekts
            routeValues: new { id = newRouteStopPoint.RouteId },
            value: newRouteStopPoint
        );
    }
    
    //**********************************************************************************
    //READ-Methods
    //**********************************************************************************

    /// <summary>
    /// Retrieves all route stop points in the system.
    /// </summary>
    /// <returns>A collection of all route stop points as DTOs.</returns>
    [HttpGet]
    public async Task<ActionResult> GetAllRouteStopPoints()
    {
        var result = await routeStopPointService.GetAllRouteStopPointsAsync();
        return Ok(result.Select(r => r.ToRouteStopPointDto()));
    }

    //......................................................................

    /// <summary>
    /// Retrieves a route stop point by its unique ID.
    /// </summary>
    /// <param name="id">The unique ID of the route stop point.</param>
    /// <returns>The route stop point as a DTO, or a 404 status if not found.</returns>
    [HttpGet("by-id/{id:int}")]
    public async Task<ActionResult> GetRouteStopPointById(int id)
    {
        var result = await routeStopPointService.GetRouteStopPointByIdAsync(id);
        if (result is null)
        {
            return NotFound(StatusInfo.InvalidRouteStopPointId(id));
        }

        return Ok(result.ToRouteStopPointDto());
    }

    //......................................................................

    /// <summary>
    /// Retrieves route stop points associated with a specific route ID.
    /// </summary>
    /// <param name="routeId">The ID of the route.</param>
    /// <returns>A collection of route stop points as DTOs, or a 404 status if not found.</returns>
    [HttpGet("by-routeId/{routeId:int}")]
    public async Task<ActionResult> GetRoutStopPointsByRouteId(int routeId)
    {
        var result = await routeStopPointService.GetRouteStopPointsByRouteIdAsync(routeId);

        if (!result.Any())
        {
            return NotFound(StatusInfo.InvalidRouteId(routeId));
        }

        return Ok(result.Select(r => r.ToRouteStopPointDto()));
    }

    //......................................................................

    /// <summary>
    /// Retrieves route stop points by its arrival time.
    /// </summary>
    /// <param name="arrivalTime">The arrival time to search for.</param>
    /// <returns>A collection of route stop points as DTOs, or a 404 status if not found.</returns>
    [HttpGet("by-arrivalTime/{arrivalTime}")]
    public async Task<ActionResult> GetRouteStopPointsByArrivalTime(string arrivalTime)
    {
        if (!DateTime.TryParseExact(arrivalTime,
                "dd-MM-yyyy",
                new CultureInfo("de-AT"),
                DateTimeStyles.None,
                out var parsedArrivalTime))
        {
            return BadRequest("Invalid date format for arrivalTime.");
        }

        var result = await routeStopPointService.GetRouteStopPointsByArrivalTimeAsync(parsedArrivalTime);

        if (!result.Any())
        {
            return NotFound(StatusInfo.InvalidStopPointArrivalTime(arrivalTime));
        }

        return Ok(result.Select(r => r.ToRouteStopPointDto()));
    }
    
    //......................................................................
    
    /// <summary>
    /// Retrieves route stop points by its departure time.
    /// </summary>
    /// <param name="departureTime">The departure time to search for.</param>
    /// <returns>A collection of route stop points as DTOs, or a 404 status if not found.</returns>
    [HttpGet("by-departureTime/{departureTime}")]
    public async Task<ActionResult> GetRouteStopPointByDepartureTime(string departureTime)
    {
        if (!DateTime.TryParseExact(departureTime, 
                "dd-MM-yyyy",
                new CultureInfo("de-AT"),
                DateTimeStyles.None,out var parsedDepartureTime))
        {
            return BadRequest("Invalid date format for departureTime.");
        }

        var result = await routeStopPointService.GetRouteStopPointsByDepartureTimeAsync(parsedDepartureTime);

        if (!result.Any())
        {
            return NotFound(StatusInfo.InvalidStopPointDepartureTime(departureTime));
        }

        return Ok(result.Select(r => r.ToRouteStopPointDto()));
    }

    //......................................................................

    /// <summary>
    /// Retrieves route stop points by the name of their associated route.
    /// </summary>
    /// <param name="routeName">The name of the route.</param>
    /// <returns>A collection of route stop points as DTOs, or a 404 status if not found.</returns>
    [HttpGet("by-routeName/{routeName}")]
    public async Task<ActionResult> GetRouteStopPointsByRouteName(string routeName)
    {
        var result = await routeStopPointService.GetRouteStopPointsByRouteNameAsync(routeName);

        if (!result.Any())
        {
            return NotFound(StatusInfo.InvalidRouteName(routeName));
        }

        return Ok(result.Select(r => r.ToRouteStopPointDto()));
    }
    
    //......................................................................

    /// <summary>
    /// Determines if two stop points are on the same route.
    /// </summary>
    /// <param name="startStopPointName">The name of the start stop point.</param>
    /// <param name="endStopPointName">The name of the end stop point.</param>
    /// <returns>A boolean indicating whether the stop points share the same route.</returns>
    [HttpGet("is-same-route/{startStopPointName}/{endStopPointName}")]
    public async Task<ActionResult> IsSameRouteForRouteStopPoints(string startStopPointName, string endStopPointName)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(startStopPointName) || string.IsNullOrWhiteSpace(endStopPointName))
            {
                return BadRequest("Both startStopPointName and endStopPointName must be provided.");
            }
            
            var result = await routeStopPointService.IsSameRouteForRouteStopPoints(startStopPointName, endStopPointName);
            
            return Ok(new { SameRoute = result });
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {e.Message}");
        }
    }
    
    
    
}