using NextStop.Domain;

namespace NextStop.Api.DTOs;


public record RouteStopPointDto
{
    public int Id { get; init; }
    
    public int RouteId { get; set; }

    public int StopPointId { get; set; }
    public DateTime ArrivalTime { get; set; }
    public DateTime DepartureTime { get; set; }
    public int Order { get; set; }
    
}

public record RouteStopPointForCreationDto
{
    public int Id { get; init; }
    
    public int RouteId { get; set; }

    public int StopPointId { get; set; }
    public DateTime ArrivalTime { get; set; }
    public DateTime DepartureTime { get; set; }
    public int Order { get; set; }

    public RouteStopPoint ToRouteStopPoint()
    {
        return new RouteStopPoint
        {
            RouteId = this.Id,
            StopPointId = this.StopPointId,
            ArrivalTime = this.ArrivalTime,
            DepartureTime = this.DepartureTime,
            Order = this.Order
        };
    }
}