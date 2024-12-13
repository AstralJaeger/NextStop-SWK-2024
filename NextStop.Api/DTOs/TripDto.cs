using System.ComponentModel.DataAnnotations;
using NextStop.Domain;

namespace NextStop.Api.DTOs;

public record TripDto
{
    public int Id { get; init; }
    
    public int RouteId { get; set; }
    
    public int VehicleId { get; set; }

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

public record TripForCreationDto
{
    public int Id { get; init; }
    
    public int RouteId { get; set; }
    
    public int VehicleId { get; set; }
    
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