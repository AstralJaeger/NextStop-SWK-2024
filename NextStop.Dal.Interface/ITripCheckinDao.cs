using NextStop.Api.DTOs;
using NextStop.Domain;

namespace NextStop.Common;

/// <summary>
/// Data Access Object interface for handling Trip Check-In operations.
/// </summary>
public interface ITripCheckinDao
{


    /// <summary>
    /// Adds a new Trip Check-In record to the database.
    /// </summary>
    /// <param name="tripCheckIn">The Trip Check-In object to add.</param>
    /// <returns>The ID of the newly created Trip Check-In record.</returns>
    Task<int> InsertTripCheckinAsync(TripCheckin tripCheckIn);

    //**********************************************************************************
    //**********************************************************************************

    /// <summary>
    /// Retrieves all Trip Check-In records from the database.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation,
    /// containing a list of all Trip Check-In objects.</returns>
    Task<IEnumerable<TripCheckin>> GetAllTripCheckinsAsync();

    //----------------------------------------------------------------------------------

    /// <summary>
    /// Retrieves a Trip Check-In record by its unique ID.
    /// </summary>
    /// <param name="checkInId">The unique ID of the Trip Check-In.</param>
    /// <returns>The Trip Check-In object with the specified ID, or null if not found.</returns>
    Task<TripCheckin?> GetTripCheckinByIdAsync(int checkInId);

    //----------------------------------------------------------------------------------

    /// <summary>
    /// Retrieves all Trip Check-In records associated with a specific Trip ID.
    /// </summary>
    /// <param name="tripId">The ID of the Trip.</param>
    /// <returns>A list of Trip Check-In objects for the specified Trip ID.</returns>
    Task<IEnumerable<TripCheckin>> GetTripCheckinsByTripIdAsync(int tripId);

    //----------------------------------------------------------------------------------

    /// <summary>
    /// Retrieves all Trip Check-In records associated with a specific Stop Point ID.
    /// </summary>
    /// <param name="stopPointId">The ID of the Stop Point.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing
    /// a list of Trip Check-In objects for the specified Stop Point ID.</returns>
    Task<IEnumerable<TripCheckin>> GetTripCheckinsByStopPointIdAsync(int stopPointId);

    //----------------------------------------------------------------------------------

    /// <summary>
    /// Retrieves all Trip Check-In records for a specific check-in timestamp.
    /// </summary>
    /// <param name="checkIn">The check-in timestamp to filter by.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing
    /// a list of Trip Check-In objects with the specified check-in timestamp.</returns>
    Task<IEnumerable<TripCheckin>> GetTripCheckinsByCheckin(DateTime checkIn);

    //----------------------------------------------------------------------------------

    /// <summary>
    /// Retrieves the planned arrival time for a specific stop point on a specific route.
    /// </summary>
    /// <param name="routeStopPointId">The ID of the route stop point.</param>
    /// <returns>The planned arrival time as a <see cref="DateTime"/>.</returns>
    /// <exception cref="InvalidOperationException">Thrown if no matching entry is found.</exception>
    Task<DateTime> GetArrivalTimeByRouteStopPointAsync(int routeStopPointId);
    
    //----------------------------------------------------------------------------------

    /// <summary>
    /// Retrieves the route ID associated with a specific trip ID.
    /// </summary>
    /// <param name="tripId">The ID of the trip.</param>
    /// <returns>The route ID as an <see cref="int"/>.</returns>
    /// <exception cref="InvalidOperationException">Thrown if no matching trip is found.</exception>
    Task<int> GetRouteIdByTripIdAsync(int tripId);
    
    //----------------------------------------------------------------------------------

    /// <summary>
    /// Retrieves detailed delay statistics for a specific trip.
    /// </summary>
    /// <param name="tripId">The unique identifier of the trip.</param>
    /// <returns>
    /// A task representing the asynchronous operation. The task result contains a 
    /// <see cref="TripDelayStatistics"/> object with the following details:
    /// - Average delay across all recorded check-ins.
    /// - Total number of distinct stop points.
    /// - Percentage of stop points that were on time, slightly late, late, or very late.
    /// Returns <c>null</c> if no data is found for the specified trip.
    /// </returns>
    /// <remarks>
    /// The delay is categorized as follows:
    /// - On time: Delay is less than 2 minutes.
    /// - Slightly late: Delay is between 2 and 5 minutes.
    /// - Late: Delay is between 5 and 10 minutes.
    /// - Very late: Delay exceeds 10 minutes.
    ///
    /// The percentages are calculated based on the total number of distinct stop points associated with the trip.
    /// </remarks>
    /// <exception cref="ArgumentException">
    /// Thrown if the provided <paramref name="tripId"/> is not valid.
    /// </exception>
    Task<TripDelayStatistics?> GetTripDelayStatisticsAsync(int tripId);
}
