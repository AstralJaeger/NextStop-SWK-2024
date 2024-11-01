namespace NextStop.Domain;

/// <summary>
/// Represents a route that consists of multiple stop points and operates within a specific validity period.
/// </summary>
public class Route
{
    /// <summary>
    /// Gets or sets the unique identifier for the route.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the route.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the start date of the route's validity period.
    /// </summary>
    public DateTime ValidFrom { get; set; }

    /// <summary>
    /// Gets or sets the end date of the route's validity period.
    /// </summary>
    public DateTime ValidTo { get; set; }

    /// <summary>
    /// Gets or sets the days on which the route is valid.
    /// </summary>
    public int ValidOn { get; set; }

    /// <summary>
    /// Navigation property for accessing the route's stop points with additional details.
    /// </summary>
    public List<RouteStopPoint> RouteStopPoints { get; set; } = new List<RouteStopPoint>();
    //public List<StopPoint> RouteStopPoints { get; set; } = new List<StopPoint>();

}