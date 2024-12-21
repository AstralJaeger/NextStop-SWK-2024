namespace NextStop.Api.DTOs;

/// <summary>
/// Represents delay statistics for a specific trip.
/// </summary>
public class TripDelayStatistics
{
    /// <summary>
    /// Unique identifier for the trip.
    /// </summary>
    public int TripId { get; init; }

    /// <summary>
    /// Average delay in minutes.
    /// </summary>
    public double AverageDelay { get; init; }

    /// <summary>
    /// Total number of stop points for the trip.
    /// </summary>
    public int TotalStopPoints { get; init; }

    /// <summary>
    /// Percentage of stop points that were on time (<2 minutes delay).
    /// </summary>
    public double OnTimePercentage { get; init; }

    /// <summary>
    /// Percentage of stop points with a slight delay (2-5 minutes).
    /// </summary>
    public double SlightlyLatePercentage { get; init; }

    /// <summary>
    /// Percentage of stop points with a delay (5-10 minutes).
    /// </summary>
    public double LatePercentage { get; init; }

    /// <summary>
    /// Percentage of stop points with a significant delay (>10 minutes).
    /// </summary>
    public double VeryLatePercentage { get; init; }
}