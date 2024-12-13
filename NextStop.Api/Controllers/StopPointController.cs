using Microsoft.AspNetCore.Mvc;
using NextStop.Api.DTOs;
using NextStop.Api.Mappers;
using NextStop.Domain;
using NextStop.Service.Interfaces;

namespace NextStop.Api.Controllers;

[ApiController]
//[ProducesResponseType(StatusCodes.Status200OK)] 
[Route("api/[controller]")]
public class StopPointController: Controller
{
    private readonly IStopPointService stopPointService;

    public StopPointController(IStopPointService stopPointService)
    {
        this.stopPointService = stopPointService ?? throw new ArgumentNullException(nameof(stopPointService));
    }
    
    //todo Fehlerbehandlung
    
    [HttpGet]
    public async Task<ActionResult> GetAllStopPoints()
    {
       
        var result = await stopPointService.GetAllStopPointsAsync();
        return Ok(result.Select(r => r.ToStopPointDto()));
    }

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

    // [HttpGet("routes-by-spId/{id:int}")]
    // public async Task<ActionResult> GetRoutesByStopPointId(int stopPointId)
    // {
    //     var result = await stopPointService.GetRoutesByStopPointAsync(stopPointId);
    //     if (result is null)
    //     {
    //         return NotFound(StatusInfo.NoRoutesFoundForStopPoint(stopPointId));
    //     }
    //
    //     return Ok(result.ToRouteDto());
    // }
    

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


    [HttpGet("by-stoppoint/{stopPointId:int}")]
    public async Task<ActionResult> GetRoutesByStopPoint(int stopPointId)
    {
        var routes = await stopPointService.GetRoutesByStopPointAsync(stopPointId);

        if (routes == null || !routes.Any())
        {
            return NotFound(StatusInfo.InvalidStopPointId(stopPointId));
        }

        return Ok(routes.Select(r => r.ToRouteDto()));
    }


    [HttpPost]
    [Produces("application/json", "text/plain")]
    public async Task<ActionResult<StopPointDto>> InsertStopPoint(StopPointForCreationDto stopPointDto)
    {
        if (stopPointDto.Id == 0 && await stopPointService.StopPointAlreadyExists(stopPointDto.Id))
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


    [HttpPut("update/{stopPointId:int}")]
    public async Task<ActionResult> UpdateStopPoint(int stopPointId, StopPointForUpdateDto stopPointDto)
    {
        var existingStopPoint = await stopPointService.GetStopPointByIdAsync(stopPointId);
        if (existingStopPoint is null)
        {
            return NotFound();
        }
        
        stopPointDto.UpdateStopPoint(existingStopPoint);
        
        await stopPointService.UpdateStopPointAsync(existingStopPoint);
        
        return NoContent();
    }
    
    [HttpDelete("delete/{id:int}")]
    public async Task<ActionResult> DeleteStopPoint(int id)
    {
        if (await stopPointService.DeleteStopPointAsync(id))
        {
            return NoContent();
        }
        else
        {
            return NotFound();
        }
    }
    
}