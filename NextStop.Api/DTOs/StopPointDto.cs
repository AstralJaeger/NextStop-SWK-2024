using System.ComponentModel.DataAnnotations;
using NextStop.Domain;

namespace NextStop.Api.DTOs;

public record StopPointDto
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string ShortName { get; init; }
    public CoordinatesDto Location { get; init; }
    
}


public record StopPointForCreationDto
{
    public int Id { get; init; }
    
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, ErrorMessage = "Name must be between 1 and 100 characters.")]
    public string Name { get; init; }
    
    [Required(ErrorMessage = "ShortName is required.")]
    [StringLength(100, ErrorMessage = "ShortName must be 3 characters long.")]
    public string ShortName { get; init; }
    public CoordinatesDto Location { get; init; }

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


public record CoordinatesDto
{
    public double Latitude { get; init; }
    public double Longitude { get; init; }
}


public record StopPointForUpdateDto
{
        
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, ErrorMessage = "Name must be between 1 and 100 characters.")]
    public string Name { get; init; }
    
    [Required(ErrorMessage = "ShortName is required.")]
    [StringLength(100, ErrorMessage = "ShortName must be 3 characters long.")]
    public string ShortName { get; init; }
    public CoordinatesDto Location { get; init; }
    
    
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

