using Microsoft.AspNetCore.Mvc;
using NextStop.Api.DTOs;
using NextStop.Api.Mappers;
using NextStop.Service.Interfaces;

namespace NextStop.Api.Controllers;

[ApiController]
//[ProducesResponseType(StatusCodes.Status200OK)] 
[Route("api/[controller]")]

public class RouteStopPointController : ControllerBase
{
    private readonly IRouteStopPointService routeStopPointService;
    //private readonly IStopPointService stopPointService;

    public RouteStopPointController(IRouteStopPointService routeStopPointService)
    {
        this.routeStopPointService = routeStopPointService ?? throw new ArgumentNullException(nameof(routeStopPointService));
    }

    [HttpGet]
    public async Task<ActionResult> GetAllRouteStopPoints()
    {
        var result = await routeStopPointService.GetAllRouteStopPointsAsync();
        return Ok(result.Select(r => r.ToRouteStopPointDto()));
    }

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

    [HttpGet("by-routeId/{routeId:int}")]
    public async Task<ActionResult> GetRoutStopPointsByRouteId(int routeId)
    {
        var result = await routeStopPointService.GetRouteStopPointsByRouteIdAsync(routeId);

        if (result is null)
        {
            return NotFound(StatusInfo.InvalidRouteId(routeId));
        }

        return Ok(result.Select(r => r.ToRouteStopPointDto()));
    }
        
    [HttpGet("by-arrivalTime/{arrivalTime}")]
    public async Task<ActionResult> GetRouteStopPointsByArrivalTimeAsync(string arrivalTime)
    {
        if (!DateTime.TryParse(arrivalTime, out var parsedArrivalTime))
        {
            return BadRequest("Invalid date format for arrivalTime.");
        }

        var result = await routeStopPointService.GetRouteStopPointByArrivalTimeAsync(parsedArrivalTime);

        if (result is null)
        {
            return NotFound(StatusInfo.InvalidStopPointArrivalTime(arrivalTime));
        }

        return Ok(result.ToRouteStopPointDto());
    }
    
    

    [HttpGet("by-departureTime/{departureTime}")]
    public async Task<ActionResult> GetRouteStopPointByDepartureTimeAsync(string departureTime)
    {
        if (!DateTime.TryParse(departureTime, out var parsedDepartureTime))
        {
            return BadRequest("Invalid date format for departureTime.");
        }

        var result = await routeStopPointService.GetRouteStopPointByDepartureTimeAsync(parsedDepartureTime);

        if (result is null)
        {
            return NotFound(StatusInfo.InvalidStopPointDepartureTime(departureTime));
        }

        return Ok(result.ToRouteStopPointDto());
    }

    [HttpGet("by-routeName/{routeName}")]
    public async Task<ActionResult> GetRouteStopPointsByRouteName(string routeName)
    {
        var result = await routeStopPointService.GetRouteStopPointsByRouteNameAsync(routeName);

        if (result is null)
        {
            return NotFound(StatusInfo.InvalidRouteName(routeName));
        }

        return Ok(result.Select(r => r.ToRouteStopPointDto()));
    }
    
    
    [HttpPost]
    [Produces("application/json", "text/plain")]
    public async Task<ActionResult> InsertRouteStopPoint(RouteStopPointForCreationDto routeStopPointDto)
    {
        // Validierung des Modells
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }


        if (routeStopPointDto.Id != 0 && await routeStopPointService.RouteStopPointAlreadyExists(routeStopPointDto.Id))
        {
            return Conflict(StatusInfo.RouteStopPointAlreadyExists(routeStopPointDto.Id));
        }
        
        var newRouteStopPoint = routeStopPointDto.ToRouteStopPoint();
        await routeStopPointService.InsertRouteStopPointAsync(newRouteStopPoint);

        return CreatedAtAction(
            actionName: nameof(GetRouteStopPointById), // Endpoint für die Rückgabe des Objekts
            routeValues: new { routeId = newRouteStopPoint.RouteId },
            value: newRouteStopPoint
        );
    }
    
    [HttpGet("is-same-route/{startStopPointName}/{endStopPointName}")]
    public async Task<ActionResult> IsSameRouteForRouteStopPoints(string startStopPointName, string endStopPointName)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(startStopPointName) || string.IsNullOrWhiteSpace(endStopPointName))
            {
                return BadRequest("Both startStopPointName and endStopPointName must be provided.");
            }

            // Aufruf der Service-Methode
            var result = await routeStopPointService.IsSameRouteForRouteStopPoints(startStopPointName, endStopPointName);

            // Rückgabe des Ergebnisses
            return Ok(new { SameRoute = result });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
        }
    }
    
    
    // [HttpGet("routes-between/{startStopPointName}/{endStopPointName}")]
    // public async Task<ActionResult> GetRouteBetweenStopPoints(string startStopPointName, string endStopPointName)
    // {
    //     try
    //     {
    //         var routes = await routeStopPointService.GetRouteBetweenStopPointsAsync(startStopPointName, endStopPointName);
    //
    //         if (!routes.Any())
    //         {
    //             return NotFound($"No routes found between '{startStopPointName}' and '{endStopPointName}'.");
    //         }
    //
    //         return Ok(routes.Select(r => r.ToRouteStopPointDto()));
    //     }
    //     catch (Exception ex)
    //     {
    //         return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
    //     }
    // }
    
    
    // [HttpGet("routes-between/")]
    // public async Task<ActionResult> GetRoutesBetweenStopPoints(string startStopPointName, string endStopPointName)
    // {
    //     try
    //     {
    //         // Lösen der StopPoint-Namen in IDs
    //         var startStopPoint = await stopPointService.GetStopPointByNameAsync(startStopPointName);
    //         if (startStopPoint == null)
    //         {
    //             return NotFound($"Start StopPoint with name '{startStopPointName}' not found.");
    //         }
    //
    //         var endStopPoint = await stopPointService.GetStopPointByNameAsync(endStopPointName);
    //         if (endStopPoint == null)
    //         {
    //             return NotFound($"End StopPoint with name '{endStopPointName}' not found.");
    //         }
    //
    //         // Suchen der Routen zwischen den beiden StopPoints
    //         var routes = await routeStopPointService.GetRoutesBetweenStopPointsAsync(startStopPoint.Name, endStopPoint.Name);
    //
    //         if (routes is null)
    //         {
    //             return NotFound($"No routes found between StopPoint '{startStopPointName}' and StopPoint '{endStopPointName}'.");
    //         }
    //
    //         // Rückgabe der gefundenen Routen
    //         return Ok(routes.Select(r => r.ToRouteStopPointDto()));
    //     }
    //     catch (Exception ex)
    //     {
    //         return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
    //     }
    // }
}