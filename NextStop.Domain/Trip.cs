namespace NextStop.Domain;

/// <summary>
/// Represents a trip that follows a specific route and is associated with a particular vehicle.
/// </summary>
public class Trip
{
    /// <summary>
    /// Gets or sets the unique identifier for the trip.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the route that this trip follows.
    /// </summary>
    public int RouteId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the vehicle assigned to this trip.
    /// </summary>
    public int VehicleId { get; set; }

    /// <summary>
    /// Navigation property for accessing the associated route.
    /// </summary>
    public Route Route { get; set; }

    /// <summary>
    /// Navigation property for accessing the check-ins of this trip at different stop points.
    /// </summary>
    public List<TripCheckin> TripCheckins { get; set; } = new List<TripCheckin>();
}
