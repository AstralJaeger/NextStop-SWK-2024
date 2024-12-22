namespace NextStop.Domain;

/// <summary>
/// Represents a check-in at a specific stop point during a trip, including the check-in time.
/// </summary>
public class TripCheckin
{
    /// <summary>
    /// Gets or sets the unique identifier for the trip check-in.
    /// </summary>
    public int Id { get; set; } 

    /// <summary>
    /// Gets or sets the identifier of the associated trip.
    /// </summary>
    public int TripId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the stop point where the check-in occurs.
    /// </summary>
    public int StopPointId { get; set; }

    /// <summary>
    /// Gets or sets the check-in date and time.
    /// </summary>
    public DateTime CheckIn { get; set; }
    
    /// <summary>
    /// Gets or sets the curren delay for the routestoppoint.
    /// </summary>
    public int Delay { get; set; }
    
    /// <summary>
    /// Gets or sets the identifier of the route stop point where the check-in occurs.
    /// </summary>
    public int RouteStopPointId { get; set; }


    public TripCheckin(int id, int tripId, int stopPointId, DateTime checkin, int delay, int routeStopPointId)
    {
        Id = id;
        TripId = tripId;
        StopPointId = stopPointId;
        CheckIn = checkin;
        Delay = delay;
        RouteStopPointId = routeStopPointId;
    }
    
    public TripCheckin() {}

    public override string ToString() => $"ID: {Id}, Trip: {TripId}, StopPoint: {StopPointId}, Checkin: {CheckIn}";
}
