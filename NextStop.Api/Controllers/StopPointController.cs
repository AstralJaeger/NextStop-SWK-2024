﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NextStop.Api.DTOs;
using NextStop.Api.Mappers;
using NextStop.Domain;
using NextStop.Service.Interfaces;

namespace NextStop.Api.Controllers;

/// <summary>
/// API Controller for managing Stop Points.
/// Provides endpoints for creating, retrieving, updating, and deleting stop points.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class StopPointController: ControllerBase
{
    private readonly IStopPointService stopPointService;

    //......................................................................

    /// <summary>
    /// Initializes a new instance of the <see cref="StopPointController"/> class.
    /// </summary>
    /// <param name="stopPointService">The service for managing stop points.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="stopPointService"/> is null.</exception>
    public StopPointController(IStopPointService stopPointService)
    {
        this.stopPointService = stopPointService ?? throw new ArgumentNullException(nameof(stopPointService));
    }
    
    //**********************************************************************************
    // CREATE-Methods
    //**********************************************************************************

    /// <summary>
    /// Inserts a new stop point into the system.
    /// </summary>
    /// <param name="stopPointDto">The data for creating a new stop point.</param>
    /// <returns>The created stop point as a DTO.</returns>
    [HttpPost]
    [Authorize(Roles = "admin")]
    [Produces("application/json", "text/plain")]
    public async Task<ActionResult<StopPointDto>> InsertStopPoint(StopPointForCreationDto stopPointDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (stopPointDto.Id is not 0 && await stopPointService.StopPointAlreadyExists(stopPointDto.Id))
        {
            return Conflict(StatusInfo.StopPointAlreadyExists(stopPointDto.Id));
        }
        
        var newStopPoint = stopPointDto.ToStopPoint();
        await stopPointService.InsertStopPointAsync(newStopPoint);
        return CreatedAtAction(
            actionName: nameof(GetStopPointById), 
            routeValues: new { id = newStopPoint.Id }, 
            value: newStopPoint.ToStopPointDto());
    }
    
    //**********************************************************************************
    //READ-Methods
    //**********************************************************************************

    /// <summary>
    /// Retrieves all stop points in the system.
    /// </summary>
    /// <returns>A collection of all stop points as DTOs.</returns>
    [HttpGet]
    public async Task<ActionResult> GetAllStopPoints()
    {
        var result = await stopPointService.GetAllStopPointsAsync();
        return Ok(result.Select(r => r.ToStopPointDto()));
    }

    //......................................................................
    
    /// <summary>
    /// Retrieves a stop point by its unique ID.
    /// </summary>
    /// <param name="id">The unique ID of the stop point.</param>
    /// <returns>The stop point as a DTO, or a 404 status if not found.</returns>
    [HttpGet("by-id/{id:int}")]
    public async Task<ActionResult> GetStopPointById(int id)
    {
        var result = await stopPointService.GetStopPointByIdAsync(id);
        if (result is null)
        {
            return NotFound(StatusInfo.InvalidStopPointId(id));
        }
        return Ok(result.ToStopPointDto());
    }

    //......................................................................

    /// <summary>
    /// Retrieves multiple stop points by their unique ID.
    /// </summary>
    /// <param name="ids">The unique IDs of the stop points.</param>
    /// <returns>The stop points as a DTO list, or a 404 status if not found.</returns>
    [HttpPost("by-ids")]
    public async Task<ActionResult> GetStopPointById([FromBody] IEnumerable<int> ids)
    {
        var tasks = new List<Task<StopPoint?>>();
        foreach (var id in ids)
        {
            tasks.Add(stopPointService.GetStopPointByIdAsync(id));
        }

        StopPoint?[] result = await Task.WhenAll(tasks);
        if (result.All(r => r is not null))
        {
            return Ok(result.Select(r => r?.ToStopPointDto()));
        }
        return NotFound(StatusInfo.InvalidStopPointIds());
    }

    //......................................................................

    /// <summary>
    /// Retrieves a stop point by its name.
    /// </summary>
    /// <param name="name">The name of the stop point.</param>
    /// <returns>The stop point as a DTO, or a 404 status if not found.</returns>
    [HttpGet("by-name/{name}")]
    public async Task<ActionResult> GetStopPointByName(string name)
    {
        var result = await stopPointService.GetStopPointByNameAsync(name);
        if (result is null)
        {
            return NotFound(StatusInfo.InvalidStopPointName(name));
        }
        return Ok(result.ToStopPointDto());
    }
    
    //......................................................................

    /// <summary>
    /// Retrieves a stop point by its short name.
    /// </summary>
    /// <param name="shortName">The short name of the stop point.</param>
    /// <returns>The stop point as a DTO, or a 404 status if not found.</returns>
    [HttpGet("by-shortName/{shortName}")]
    public async Task<ActionResult> GetStopPointByShortName(string shortName)
    {
        var result = await stopPointService.GetStopPointByShortNameAsync(shortName);
        if (result is null)
        {
            return NotFound(StatusInfo.InvalidStopPointShortName(shortName));
        }
        return Ok(result.ToStopPointDto());
    }

    //......................................................................

    /// <summary>
    /// Retrieves all routes that are associated with a specific stop point.
    /// </summary>
    /// <param name="stopPointId">The ID of the stop point.</param>
    /// <returns>A collection of routes as DTOs, or a 404 status if no routes are found.</returns>
    [HttpGet("by-stoppoint/{stopPointId:int}")]
    public async Task<ActionResult> GetRoutesByStopPoint(int stopPointId)
    {
        var routes = await stopPointService.GetRoutesByStopPointAsync(stopPointId);

        if (!routes.Any())
        {
            return NotFound(StatusInfo.InvalidStopPointId(stopPointId));
        }

        return Ok(routes.Select(r => r.ToRouteDto()));
    }

    //......................................................................

    /// <summary>
    /// Retrieves all stoppoints near the location.
    /// </summary>
    /// <param name="latitude">The latitude of the location.</param>
    /// <param name="longitude">The longitude of the location.</param>
    /// <param name="radius">The latitude of the location.</param>
    /// <returns>A collection of routes as DTOs, or a 404 status if no routes are found.</returns>
    [HttpGet("by-coordinates/")]
    public async Task<ActionResult> GetStopPointByCoordinate([FromQuery] double longitude, [FromQuery] double latitude, [FromQuery] double radius)
    {
        if (latitude < -90 || latitude > 90 || longitude < -180 || longitude > 180)
        {
            return BadRequest("Invalid latitude or longitude values.");
        }

        if (radius < 100 && radius > 10_000)
        {
            return BadRequest("Radius must be between 100 and 10_000 meters");
        }
        
        var stoppoints = await stopPointService.GetStopPointByCoordinatesAsync(latitude, longitude, radius);

        if (!stoppoints.Any())
        {
            return NotFound(StatusInfo.StopPointNotFound(latitude, longitude, radius));
        }

        return Ok(stoppoints.Select(r => r.ToStopPointDto()));
    }

    /// <summary>
    /// Retrieves all stoppoints near the location.
    /// </summary>
    /// <param name="q">The query string</param>
    /// <returns>A collection of stoppoint's as DTOs, or a 404 status if no stoppoints are found.</returns>
    [HttpGet("by-query/")]
    public async Task<ActionResult> GetStopPointByCoordinate([FromQuery] string q)
    {
        var stoppoints = await stopPointService.QueryStopPointAsync(q);

        if (!stoppoints.Any())
        {
            return NotFound(StatusInfo.StopPointNoResults(q));
        }

        return Ok(stoppoints.Select(r => r.ToStopPointDto()));
    }
    
    //**********************************************************************************
    //UPDATE-Methods
    //**********************************************************************************

    /// <summary>
    /// Updates an existing stop point.
    /// </summary>
    /// <param name="stopPointId">The ID of the stop point to update.</param>
    /// <param name="stopPointDto">The data for updating the stop point.</param>
    /// <returns>A 204 No Content response if successful, or a 404 status if the stop point is not found.</returns>
    [HttpPut("update/{stopPointId:int}")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> UpdateStopPoint(int stopPointId, StopPointForUpdateDto stopPointDto)
    {
        var existingStopPoint = await stopPointService.GetStopPointByIdAsync(stopPointId);
        if (existingStopPoint is null)
        {
            return NotFound(StatusInfo.InvalidStopPointId(stopPointId));
        }
        
        stopPointDto.UpdateStopPoint(existingStopPoint);
        
        await stopPointService.UpdateStopPointAsync(existingStopPoint);
        
        return NoContent();
    }
    
    //**********************************************************************************
    // DELETE-Methods
    //**********************************************************************************

    /// <summary>
    /// Deletes a stop point by its unique ID.
    /// </summary>
    /// <param name="id">The unique ID of the stop point to delete.</param>
    /// <returns>A 204 No Content response if successful, or a 404 status if the stop point is not found.</returns>
    [HttpDelete("delete/{id:int}")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> DeleteStopPoint(int id)
    {
        if (await stopPointService.DeleteStopPointAsync(id))
        {
            return NoContent();
        }
        return NotFound(StatusInfo.InvalidStopPointId(id));
    }
    
}