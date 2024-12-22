using System.ComponentModel.DataAnnotations;
using NextStop.Domain;
using Route = NextStop.Domain.Route;

namespace NextStop.Api.DTOs;

public record RouteWithStopPointsForCreationDto
{
    [Required(ErrorMessage = "Route name is required.")]
    [StringLength(100, ErrorMessage = "Route name must be between 1 and 100 characters.")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "ValidFrom is required.")]
    public required DateTime ValidFrom { get; set; }

    [Required(ErrorMessage = "ValidTo is required.")]
    public required DateTime ValidTo { get; set; }

    [Required(ErrorMessage = "ValidOn is required.")]
    public required int ValidOn { get; set; }

    [Required(ErrorMessage = "At least one StopPoint is required.")]
    public required List<RouteStopPointForCreationDto> StopPoints { get; set; }
    
    
    
    /// <summary>
    /// Converts RouteWithStopPointsForCreationDto to a list of RouteStopPoint entities.
    /// </summary>
    /// <param name="dto">The source DTO.</param>
    /// <param name="routeId">The ID of the associated route (must be set after the route is created).</param>
    /// <returns>A list of RouteStopPoint entities.</returns>
    public List<RouteStopPoint> ToRouteStopPoints(int routeId)
    {
        return this.StopPoints.Select((stopPointDto, index) => new RouteStopPoint
        {
            RouteId = routeId,
            StopPointId = stopPointDto.StopPointId,
            ArrivalTime = stopPointDto.ArrivalTime,
            DepartureTime = stopPointDto.DepartureTime,
            Order = index + 1, // Automatische Sortierung nach Reihenfolge im DTO
            ValidOn = stopPointDto.ValidOn,
        }).ToList();
    }
    
    
    
    /// <summary>
    /// Converts RouteWithStopPointsForCreationDto to a Route entity.
    /// </summary>
    /// <param name="dto">The source DTO.</param>
    /// <returns>The extracted Route entity.</returns>
    public Route ToRoute()
    {
        return new Route
        {
            Name = this.Name,
            ValidFrom = this.ValidFrom,
            ValidTo = this.ValidTo,
            ValidOn = this.ValidOn
        };
    }
}

