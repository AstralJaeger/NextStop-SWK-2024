namespace NextStop.Domain;

/// <summary>
/// Represents a specific stop point along a route, including details about its location and name.
/// </summary>
public class StopPoint
{
    /// <summary>
    /// Gets or sets the unique identifier for the stop point.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the stop point.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the short name of the stop point.
    /// </summary>
    public string ShortName { get; set; }

    /// <summary>
    /// Gets or sets the geographical location of the stop point.
    /// </summary>
    public Coordinates Location { get; set; }

    /// <summary>
    /// Navigation property for accessing routes that pass through this stop point.
    /// </summary>
    public List<Route> StopPointRoutes { get; set; } = new List<Route>();
    //public List<RouteStopPoint> StopPointRoutes { get; set; } = new List<RouteStopPoint>(); ???
}