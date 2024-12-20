using System.ComponentModel.DataAnnotations;
using NextStop.Domain;
namespace NextStop.Api.DTOs;

/// <summary>
/// Data Transfer Object (DTO) for representing a trip check-in.
/// </summary>
public record TripCheckinDto
{
    /// <summary>
    /// Gets the unique ID of the trip check-in.
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// Gets or sets the ID of the associated trip.
    /// </summary>
    [Required(ErrorMessage = "TripId is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "TripId must be a positive number.")]
    public int TripId { get; set; }
    
    /// <summary>
    /// Gets or sets the ID of the associated stop point.
    /// </summary>
    [Required(ErrorMessage = "StopPointId is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "StopPointId must be a positive number.")]
    public int StopPointId { get; set; }
    
    /// <summary>
    /// Gets or sets the check-in time.
    /// </summary>
    [Required(ErrorMessage = "CheckIn time is required.")]
    [DataType(DataType.DateTime, ErrorMessage = "CheckIn must be a valid date and time.")]
    public required DateTime CheckIn { get; init; }
    
    /// <summary>
    /// Gets or sets the delay.
    /// </summary>
    [Required(ErrorMessage = "Delay is required.")]
    public int Delay { get; init; }
    
    /// <summary>
    /// Converts the DTO into a <see cref="TripCheckin"/> domain object.
    /// </summary>
    /// <returns>A <see cref="TripCheckin"/> object representing the DTO data.</returns>
    public TripCheckin ToTripCheckin()
    {
        return new TripCheckin
        {
            Id = this.Id,
            TripId = this.TripId,
            StopPointId = this.StopPointId,
            CheckIn = this.CheckIn,
            Delay = this.Delay
        };

    }
}