using System.ComponentModel.DataAnnotations;
using NextStop.Domain;

namespace NextStop.Api.DTOs;

/// <summary>
/// Data Transfer Object (DTO) for representing a route stop point.
/// </summary>
public record RouteStopPointDto
{
    /// <summary>
    /// Gets the unique ID of the route stop point.
    /// </summary>
    public int Id { get; init; }
    
    /// <summary>
    /// Gets or sets the ID of the route associated with this stop point.
    /// </summary>
    [Required(ErrorMessage = "RouteId is required.")]
    public int RouteId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the stop point.
    /// </summary>
    [Required(ErrorMessage = "StopPointId is required.")]
    public int StopPointId { get; set; }
    
    /// <summary>
    /// Gets or sets the arrival time at the stop point.
    /// </summary>
    [Required(ErrorMessage = "ArrivalTime is required.")]
    [DataType(DataType.DateTime, ErrorMessage = "ArrivalTime must be a valid date and time.")]
    public DateTime ArrivalTime { get; set; }
    
    /// <summary>
    /// Gets or sets the departure time from the stop point.
    /// </summary>
    [Required(ErrorMessage = "DepartureTime is required.")]
    [DataType(DataType.DateTime, ErrorMessage = "DepartureTime must be a valid date and time.")]
    [DateGreaterThan("ArrivalTime", ErrorMessage = "DepartureTime must be later than ArrivalTime.")]
    public DateTime DepartureTime { get; set; }
    
    /// <summary>
    /// Gets or sets the order number of the stop point on the route.
    /// </summary>
    [Required(ErrorMessage = "Order is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Order must be a positive integer.")]
    public int Order { get; set; }
    
}

/// <summary>
/// DTO for creating a new route stop point.
/// Includes methods for converting to a <see cref="RouteStopPoint"/> domain object.
/// </summary>
public record RouteStopPointForCreationDto
{
    /// <summary>
    /// Gets the unique ID of the route stop point to be created.
    /// </summary>
    public int Id { get; init; }
    
    /// <summary>
    /// Gets or sets the ID of the route associated with this stop point.
    /// </summary>
    [Required(ErrorMessage = "RouteId is required.")]
    public int RouteId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the stop point.
    /// </summary>
    [Required(ErrorMessage = "StopPointId is required.")]
    public int StopPointId { get; set; }
    
    
    /// <summary>
    /// Gets or sets the arrival time at the stop point.
    /// </summary>
    [Required(ErrorMessage = "ArrivalTime is required.")]
    [DataType(DataType.DateTime, ErrorMessage = "ArrivalTime must be a valid date and time.")]
    public DateTime ArrivalTime { get; set; }
    
    /// <summary>
    /// Gets or sets the departure time from the stop point.
    /// </summary>
    [Required(ErrorMessage = "DepartureTime is required.")]
    [DataType(DataType.DateTime, ErrorMessage = "DepartureTime must be a valid date and time.")]
    [DateGreaterThan("ArrivalTime", ErrorMessage = "DepartureTime must be later than ArrivalTime.")]
    public DateTime DepartureTime { get; set; }
    
    /// <summary>
    /// Gets or sets the order number of the stop point on the route.
    /// </summary>
    [Required(ErrorMessage = "Order is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Order must be a positive integer.")]
    public int Order { get; set; }

    /// <summary>
    /// Converts the DTO into a <see cref="RouteStopPoint"/> domain object.
    /// </summary>
    /// <returns>A <see cref="RouteStopPoint"/> object representing the DTO data.</returns>
    public RouteStopPoint ToRouteStopPoint()
    {
        return new RouteStopPoint
        {
            RouteId = this.RouteId,
            StopPointId = this.StopPointId,
            ArrivalTime = this.ArrivalTime,
            DepartureTime = this.DepartureTime,
            Order = this.Order
        };
    }
}