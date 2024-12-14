using Microsoft.AspNetCore.Mvc;
using NextStop.Api.DTOs;
using NextStop.Api.Mappers;
using NextStop.Domain;
using NextStop.Service.Interfaces;


namespace NextStop.Api.Controllers;

/// <summary>
/// API Controller for managing trip check-ins.
/// Provides endpoints for creating, retrieving, and managing trip check-in records.
/// </summary>
[ApiController]
[Route("api/[controller]")]

public class TripCheckInController: ControllerBase
{
    private readonly ITripCheckInService tripCheckInService;

    //......................................................................
    
    /// <summary>
    /// Initializes a new instance of the <see cref="TripCheckInController"/> class.
    /// </summary>
    /// <param name="tripCheckInService">The service for managing trip check-ins.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="tripCheckInService"/> is null.</exception>
    public TripCheckInController(ITripCheckInService tripCheckInService)
    {
        this.tripCheckInService = tripCheckInService ?? throw new ArgumentNullException(nameof(tripCheckInService));
    }
    
    //**********************************************************************************
    // CREATE-Methods
    //**********************************************************************************

    /// <summary>
    /// Inserts a new trip check-in record into the system.
    /// </summary>
    /// <param name="tripCheckinDto">The data transfer object for creating a trip check-in.</param>
    /// <returns>The created trip check-in as a DTO.</returns>
    [HttpPost]
    [Produces("application/json", "text/plain")]
    public async Task<ActionResult<TripCheckinDto>> InsertTripChekin(TripCheckinDto tripCheckinDto)

    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (tripCheckinDto.Id is not 0 && await tripCheckInService.TripCheckinAlreadyExists(tripCheckinDto.Id))
        {
            return Conflict(StatusInfo.TripCheckinAlreadyExists(tripCheckinDto.Id));
            
        }
        
        var newTripCheckin = tripCheckinDto.ToTripCheckin();
        await tripCheckInService.InsertTripCheckinAsync(newTripCheckin);

        return CreatedAtAction(
            actionName: nameof(GetTripCheckInById), 
            routeValues: new { id = newTripCheckin.Id },
            value: newTripCheckin.ToTripCheckinDto()
        );

    }
    
    //**********************************************************************************
    //READ-Methods
    //**********************************************************************************

    /// <summary>
    /// Retrieves all trip check-ins in the system.
    /// </summary>
    /// <returns>A collection of all trip check-ins as DTOs.</returns>
    [HttpGet]
    public async Task<ActionResult> GetAllTripCheckIns()
    {
        var result = await tripCheckInService.GetAllTripCheckinsAsync();
        return Ok(result.Select(r => r.ToTripCheckinDto()));
    }
    
    //......................................................................

    /// <summary>
    /// Retrieves a trip check-in record by its unique ID.
    /// </summary>
    /// <param name="id">The unique ID of the trip check-in.</param>
    /// <returns>The trip check-in as a DTO, or a 404 status if not found.</returns>
    [HttpGet("by-id/{id:int}")]
    public async Task<ActionResult> GetTripCheckInById(int id)
    {
        var result = await tripCheckInService.GetTripCheckinByIdAsync(id);
        if (result is null)
        {
            return NotFound(StatusInfo.InvalidTripCheckinId(id));
        }

        return Ok(result.ToTripCheckinDto());
    }
    
    //......................................................................

    /// <summary>
    /// Retrieves all trip check-ins associated with a specific trip.
    /// </summary>
    /// <param name="tripId">The ID of the trip.</param>
    /// <returns>A collection of trip check-ins as DTOs, or a 404 status if no check-ins are found.</returns>
    [HttpGet("by-tripId/{tripId:int}")]
    public async Task<ActionResult> GetTripChecksInsByTripId(int tripId)
    {
        var result = await tripCheckInService.GetTripCheckinsByTripIdAsync(tripId);
        if (!result.Any())
        {
            return NotFound(StatusInfo.InvalidTripId(tripId));
        }

        return Ok(result.Select(r => r.ToTripCheckinDto()));
    }
    
    //......................................................................

    /// <summary>
    /// Retrieves all trip check-ins associated with a specific stop point.
    /// </summary>
    /// <param name="stopPointId">The ID of the stop point.</param>
    /// <returns>A collection of trip check-ins as DTOs, or a 404 status if no check-ins are found.</returns>
    [HttpGet("by-stopPointId/{stopPointId:int}")]
    public async Task<ActionResult> GetTripCheckInsByStopPointId(int stopPointId)
    {
        var result = await tripCheckInService.GetTripCheckinsByStopPointIdAsync(stopPointId);
        if (!result.Any())
        {
            return NotFound(StatusInfo.InvalidStopPointId(stopPointId));
        }

        return Ok(result.Select(r => r.ToTripCheckinDto()));
    }

    //......................................................................

    /// <summary>
    /// Retrieves all trip check-ins for a specific check-in date.
    /// </summary>
    /// <param name="checkin">The check-in date to filter by.</param>
    /// <returns>A collection of trip check-ins as DTOs, or a 404 status if no check-ins are found.</returns>
    [HttpGet("by-checkin/{checkin}")]
    public async Task<ActionResult> GetTripCheckInsByCheckin(string checkin)
    {
        if (!DateTime.TryParse(checkin, out var checkinDate))
        {
            return BadRequest("Invalid check-in date format. Please provide a valid date.");
        }
        
        var result = await tripCheckInService.GetTripCheckinsByCheckin(checkinDate);
        
        if (!result.Any())
        {
            return NotFound($"No trip check-ins found for check-in date: {checkin}.");
        }
        
        return Ok(result.Select(r => r.ToTripCheckinDto()));
    }
}