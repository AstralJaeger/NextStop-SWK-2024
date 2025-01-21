using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NextStop.Api.DTOs;
using NextStop.Api.Mappers;
using NextStop.Domain;
using NextStop.Service.Interfaces;

namespace NextStop.Api.Controllers;

/// <summary>
/// API Controller for managing trips.
/// Provides endpoints for creating, retrieving, and managing trip records.
/// </summary>
[ApiController]
[Route("api/[controller]")]

public class TripController: ControllerBase
{
    private readonly ITripService tripService;

    //......................................................................

    /// <summary>
    /// Initializes a new instance of the <see cref="TripController"/> class.
    /// </summary>
    /// <param name="tripService">The service for managing trips.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="tripService"/> is null.</exception>
    public TripController(ITripService tripService)
    {
        this.tripService = tripService ?? throw new ArgumentNullException(nameof(tripService));
    }

    //**********************************************************************************
    // CREATE-Methods
    //**********************************************************************************
    
    /// <summary>
    /// Inserts a new trip into the system.
    /// </summary>
    /// <param name="tripDto">The data transfer object for creating a trip.</param>
    /// <returns>The created trip as a DTO.</returns>
    [HttpPost]
    [Produces("application/json", "text/plain")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult<TripDto>> InsertTrip(TripDto tripDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (tripDto.Id != 0 && await tripService.TripAlreadyExists(tripDto.Id))
        {
            return Conflict(StatusInfo.TripAlreadyExists(tripDto.Id));
        }
        
        var newTrip = tripDto.ToTrip();
        await tripService.InsertTripAsync(newTrip);

        return CreatedAtAction(
            actionName: nameof(GetTripById),
            routeValues: new { id = newTrip.Id },
            value: newTrip.ToTripDto()
        );
    }
    
    //**********************************************************************************
    //READ-Methods
    //**********************************************************************************

    /// <summary>
    /// Retrieves all trips from the system.
    /// </summary>
    /// <returns>A collection of all trips as DTOs.</returns>
    [HttpGet]
    public async Task<ActionResult> GetAllTrips()
    {
        var result = await tripService.GetAllTripsAsync();
        return Ok(result.Select(r => r.ToTripDto()));
    }

    //......................................................................

    /// <summary>
    /// Retrieves a trip by its unique ID.
    /// </summary>
    /// <param name="id">The unique ID of the trip.</param>
    /// <returns>The trip as a DTO, or a 404 status if not found.</returns>
    [HttpGet("by-id/{id:int}")]
    public async Task<ActionResult> GetTripById(int id)
    {
        var result = await tripService.GetTripByIdAsync(id);
        if (result is null)
        {
            return NotFound(StatusInfo.InvalidTripId(id));
        }

        return Ok(result.ToTripDto());
    }
    
    //......................................................................

    /// <summary>
    /// Retrieves all trips associated with a specific route.
    /// </summary>
    /// <param name="routeId">The ID of the route.</param>
    /// <returns>A collection of trips as DTOs, or a 404 status if no trips are found.</returns>
    [HttpGet("by-routeId/{routeId:int}")]
    public async Task<ActionResult> GetTripsByRouteId(int routeId)
    {
        var result = await tripService.GetTripsByRouteIdAsync(routeId);
        if (!result.Any())
        {
            return NotFound(StatusInfo.InvalidRouteId(routeId));
        }

        return Ok(result.Select(r => r.ToTripDto()));
    }
    
    //......................................................................

    /// <summary>
    /// Retrieves all trips associated with a specific vehicle.
    /// </summary>
    /// <param name="vehicleId">The ID of the vehicle.</param>
    /// <returns>A collection of trips as DTOs, or a 404 status if no trips are found.</returns>
    [HttpGet("by-vehicleId/{vehicleId:int}")]
    public async Task<ActionResult> GetTripsByVehicleId(int vehicleId)
    {
        var result = await tripService.GetTripsByVehicleIdAsync(vehicleId);
        if (!result.Any())
        {
            return NotFound(StatusInfo.InvalidVehicleId(vehicleId));
        }

        return Ok(result.Select(r => r.ToTripDto()));
    }
}