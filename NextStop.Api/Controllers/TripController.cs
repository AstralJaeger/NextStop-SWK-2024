using Microsoft.AspNetCore.Mvc;
using NextStop.Api.DTOs;
using NextStop.Api.Mappers;
using NextStop.Domain;
using NextStop.Service.Interfaces;

namespace NextStop.Api.Controllers;

[ApiController]
//[ProducesResponseType(StatusCodes.Status200OK)] 
[Route("api/[controller]")]

public class TripController: ControllerBase
{
    private readonly ITripService tripService;

    public TripController(ITripService tripService)
    {
        this.tripService = tripService ?? throw new ArgumentNullException(nameof(tripService));
    }

    [HttpGet]
    public async Task<ActionResult> GetAllTrips()
    {
        var result = await tripService.GetAllTripsAsync();
        return Ok(result.Select(r => r.ToTripDto()));
    }

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
    
    
    [HttpGet("by-routeId/{routeId:int}")]
    public async Task<ActionResult> GetTripsByRouteId(int routeId)
    {
        var result = await tripService.GetTripsByRouteIdAsync(routeId);
        if (result is null)
        {
            return NotFound(StatusInfo.InvalidRouteId(routeId));
        }

        return Ok(result.Select(r => r.ToTripDto()));
    }
    
    [HttpGet("by-vehicleId/{vehicleId:int}")]
    public async Task<ActionResult> GetTripsByVehicleId(int vehicleId)
    {
        var result = await tripService.GetTripsByVehicleIdAsync(vehicleId);
        if (result is null)
        {
            return NotFound(StatusInfo.InvalidVehicleId(vehicleId));
        }

        return Ok(result.Select(r => r.ToTripDto()));
    }

    [HttpPost]
    [Produces("application/json", "text/plain")]
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
        
        Trip newTrip = tripDto.ToTrip();
        await tripService.InsertTripAsync(newTrip);

        return CreatedAtAction(
            actionName: nameof(GetTripById),
            routeValues: new { id = newTrip.Id },
            value: newTrip.ToTripDto()
        );
    }
    
}