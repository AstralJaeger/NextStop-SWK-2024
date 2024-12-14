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
    /// Navigation property for accessing the associated trip.
    /// </summary>
    //public required Trip Trip { get; set; }

    /// <summary>
    /// Navigation property for accessing the associated stop point.
    /// </summary>
    //public required StopPoint StopPoint { get; set; }

    public TripCheckin(int id, int tripId, int stopPointId, DateTime checkin)
    {
        Id = id;
        TripId = tripId;
        StopPointId = stopPointId;
        CheckIn = checkin;
    }
    
    public TripCheckin() {}

    public override string ToString() => $"ID: {Id}, Trip: {TripId}, StopPoint: {StopPointId}, Checkin: {CheckIn}";
}
