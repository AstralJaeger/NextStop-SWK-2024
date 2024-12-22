using Microsoft.AspNetCore.Mvc;
using NextStop.Api.Mappers;
using NextStop.Dal.Interface;
using NextStop.Service.Services;
using Routing;

namespace NextStop.Api.Controllers;

/// <summary>
/// API Controller for finding routes.
/// Provides endpoints for retrieving routes.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RoutingController : ControllerBase
{
    private readonly IRoutingService _routingService;

    public RoutingController(IRoutingService routingService)
    {
        this._routingService = routingService ?? throw new ArgumentNullException(nameof(routingService));
    }
    
    [HttpGet]
    public async Task<ActionResult> GetAllRouteStopPoints([FromQuery] int startId, [FromQuery] int destinationId, [FromQuery] DateTime time)
    {
        try
        {
            var result = await _routingService.GetConnectionAtTimeAsync(startId, destinationId, time);
            return Ok(result.Select(r => r.ToConnectionDto()));
        }
        catch (RouteNotFoundException e)
        {
            return NotFound(StatusInfo.NoConnectionFound(startId, destinationId, time));
        }
    }
}