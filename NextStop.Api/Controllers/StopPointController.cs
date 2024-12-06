using Microsoft.AspNetCore.Mvc;
using NextStop.Service.Interfaces;

namespace NextStop.Api.Controllers;

[ApiController]
//[ProducesResponseType(StatusCodes.Status200OK)] 
[Route("api/[controller]")]
public class StopPointController(IStopPointService stopPointService) : Controller
{
    private readonly IStopPointService stopPointService = stopPointService;
    
    [HttpGet]
    public async Task<ActionResult> GetAllEndPoints()
    {
        //todo Fehlerbehandlung
        var result = await stopPointService.GetAllStopPointsAsync();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetStopPointById(int id)
    {
        var result = await stopPointService.GetStopPointByIdAsync(id);
        return Ok(result);
    }
    
    
}