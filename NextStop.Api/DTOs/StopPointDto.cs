using System.ComponentModel.DataAnnotations;
using NextStop.Domain;

namespace NextStop.Api.DTOs;

/// <summary>
/// Data Transfer Object (DTO) for representing a stop point.
/// </summary>
public record StopPointDto
{
    /// <summary>
    /// Gets the unique ID of the stop point.
    /// </summary>
    public int Id { get; init; }
    
    /// <summary>
    /// Gets or sets the name of the stop point.
    /// </summary>
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, ErrorMessage = "Name must be between 1 and 100 characters.")]
    public string Name { get; init; }
    
    /// <summary>
    /// Gets or sets the short name of the stop point.
    /// </summary>
    [Required(ErrorMessage = "ShortName is required.")]
    [StringLength(3, ErrorMessage = "ShortName must be exactly 3 characters long.")]
    public string ShortName { get; init; }
    
    /// <summary>
    /// Gets or sets the geographical location of the stop point.
    /// </summary>
    [Required(ErrorMessage = "Location is required.")]
    public CoordinatesDto Location { get; init; }
    
}

/// <summary>
/// DTO for creating a new stop point.
/// Includes methods for converting to a <see cref="StopPoint"/> domain object.
/// </summary>
public record StopPointForCreationDto
{
    /// <summary>
    /// Gets the unique ID of the stop point to be created.
    /// </summary>
    public int Id { get; init; }
    
    /// <summary>
    /// Gets or sets the name of the stop point.
    /// </summary>
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, ErrorMessage = "Name must be between 1 and 100 characters.")]
    public string Name { get; init; }
    
    /// <summary>
    /// Gets or sets the short name of the stop point.
    /// </summary>
    [Required(ErrorMessage = "ShortName is required.")]
    [StringLength(100, ErrorMessage = "ShortName must be 3 characters long.")]
    public string ShortName { get; init; }
    
    /// <summary>
    /// Gets or sets the geographical location of the stop point.
    /// </summary>
    public CoordinatesDto Location { get; init; }

    /// <summary>
    /// Converts the DTO into a <see cref="StopPoint"/> domain object.
    /// </summary>
    /// <returns>A <see cref="StopPoint"/> object representing the DTO data.</returns>
    public StopPoint ToStopPoint()
    {
        return new StopPoint
        {
            Id = this.Id,
            Name = this.Name,
            ShortName = this.ShortName,
            Location = new Coordinates
            {
                Latitude = this.Location.Latitude,
                Longitude = this.Location.Longitude
            }
        };
    }
}

/// <summary>
/// DTO for geographical coordinates.
/// </summary>
public record CoordinatesDto
{
    /// <summary>
    /// Gets or sets the latitude of the stop point location.
    /// </summary>
    [Required(ErrorMessage = "Latitude is required.")]
    [Range(-90.0, 90.0, ErrorMessage = "Latitude must be between -90 and 90 degrees.")]
    public double Latitude { get; init; }
    
    /// <summary>
    /// Gets or sets the longitude of the stop point location.
    /// </summary>
    [Required(ErrorMessage = "Longitude is required.")]
    [Range(-180.0, 180.0, ErrorMessage = "Longitude must be between -180 and 180 degrees.")]
    public double Longitude { get; init; }
}

/// <summary>
/// DTO for updating an existing stop point.
/// </summary>
public record StopPointForUpdateDto
{

    /// <summary>
    /// Gets or sets the updated name of the stop point.
    /// </summary>
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, ErrorMessage = "Name must be between 1 and 100 characters.")]
    public string Name { get; init; }
    
    /// <summary>
    /// Gets or sets the updated short name of the stop point.
    /// </summary>
    [Required(ErrorMessage = "ShortName is required.")]
    [StringLength(100, ErrorMessage = "ShortName must be 3 characters long.")]
    public string ShortName { get; init; }
    
    /// <summary>
    /// Gets or sets the updated geographical location of the stop point.
    /// </summary>
    [Required(ErrorMessage = "Location is required.")]
    public CoordinatesDto Location { get; init; }
    
    /// <summary>
    /// Updates an existing <see cref="StopPoint"/> object with the data from this DTO.
    /// </summary>
    /// <param name="stopPoint">The existing stop point object to update.</param>
    /// <exception cref="ArgumentNullException">Thrown if the stopPoint parameter is null.</exception>
    public void UpdateStopPoint(StopPoint stopPoint)
    {
        if (stopPoint == null)
        {
            throw new ArgumentNullException(nameof(stopPoint));
        }

        stopPoint.Name = this.Name;
        stopPoint.ShortName = this.ShortName;
        stopPoint.Location = new Coordinates
        {
            Latitude = this.Location.Latitude,
            Longitude = this.Location.Longitude
        };
    }
}

