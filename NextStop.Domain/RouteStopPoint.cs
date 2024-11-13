namespace NextStop.Domain;

/// <summary>
/// Represents the connection between a route and a stop point, including arrival and departure times.
/// </summary>
public class RouteStopPoint
{
    /// <summary>
    /// Gets or sets the unique identifier for the route-stop point connection.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the associated stop point.
    /// </summary>
    public int StopPointId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the associated route.
    /// </summary>
    public int RouteId { get; set; }

    /// <summary>
    /// Gets or sets the arrival time at the stop point for the route.
    /// </summary>
    public DateTime ArrivalTime { get; set; }

    /// <summary>
    /// Gets or sets the departure time from the stop point for the route.
    /// </summary>
    public DateTime DepartureTime { get; set; }

    /// <summary>
    /// Gets or sets the order of the stop point within the route.
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// Navigation property for accessing the associated stop point.
    /// </summary>
    public required StopPoint? StopPoint { get; set; }

    /// <summary>
    /// Navigation property for accessing the associated route.
    /// </summary>
    public required Route Route { get; set; }

    public override string ToString() => $"ID: {Id}, StopPointId: {StopPointId}, RouteId: {RouteId}, Order {Order}, ArrivalTime: {ArrivalTime}, DepartureTime: {DepartureTime}";
}