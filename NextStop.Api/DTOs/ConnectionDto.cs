namespace NextStop.Api.DTOs;

public class ConnectionDto
{
    public DateTime Time { get; set; }
    
    public IList<RouteDto> Routes { get; set; }
    
    public RouteStopPointDto? Start { get; set; }
    
    public RouteStopPointDto? End { get; set; }
}
