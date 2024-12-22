using System.ComponentModel.DataAnnotations;
using NextStop.Domain;

namespace NextStop.Api.DTOs;

/// <summary>
/// Data Transfer Object (DTO) for representing a trip.
/// </summary>
public record TripDto
{
    /// <summary>
    /// Gets the unique ID of the trip.
    /// </summary>
    public required int Id { get; init; }
    
    /// <summary>
    /// Gets or sets the ID of the associated route.
    /// </summary>
    [Required(ErrorMessage = "RouteId is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "RouteId must be a positive number.")]
    public int RouteId { get; set; }
    
    /// <summary>
    /// Gets or sets the ID of the associated vehicle.
    /// </summary>
    [Required(ErrorMessage = "VehicleId is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "VehicleId must be a positive number.")]
    public int VehicleId { get; set; }

    /// <summary>
    /// Converts the DTO into a <see cref="Trip"/> domain object.
    /// </summary>
    /// <returns>A <see cref="Trip"/> object representing the DTO data.</returns>
    public Trip ToTrip()
    {
        return new Trip
        {
            Id = this.Id,
            RouteId = this.RouteId,
            VehicleId = this.VehicleId,
        };
    }
}

/// <summary>
/// DTO for creating a new trip.
/// Includes validation attributes for required fields.
/// </summary>
public record TripForCreationDto
{
    /// <summary>
    /// Gets the unique ID of the trip to be created.
    /// </summary>
    public int Id { get; init; }
    
    /// <summary>
    /// Gets or sets the ID of the associated route.
    /// </summary>
    [Required(ErrorMessage = "RouteId is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "RouteId must be a positive number.")]
    public int RouteId { get; set; }
    
    /// <summary>
    /// Gets or sets the ID of the associated vehicle.
    /// </summary>
    [Required(ErrorMessage = "VehicleId is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "VehicleId must be a positive number.")]
    public int VehicleId { get; set; }
    
    /// <summary>
    /// Converts the DTO into a <see cref="Trip"/> domain object.
    /// </summary>
    /// <returns>A <see cref="Trip"/> object representing the DTO data.</returns>
    public Trip ToTrip()
    {
        return new Trip
        {
            Id = this.Id,
            RouteId = this.RouteId,
            VehicleId = this.VehicleId,
        };
    }
}