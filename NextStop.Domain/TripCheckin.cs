﻿namespace NextStop.Domain;

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
    public DateTime Checkin { get; set; }

    /// <summary>
    /// Navigation property for accessing the associated trip.
    /// </summary>
    public Trip Trip { get; set; }

    /// <summary>
    /// Navigation property for accessing the associated stop point.
    /// </summary>
    public StopPoint StopPoint { get; set; }
}