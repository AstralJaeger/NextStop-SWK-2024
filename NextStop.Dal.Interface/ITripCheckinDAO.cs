using NextStop.Domain;

namespace NextStop.Common;

/// <summary>
/// Data Access Object interface for handling Trip Check-In operations.
/// </summary>
public interface ITripCheckinDAO
{
    /// <summary>
    /// Adds a new Trip Check-In record to the database.
    /// </summary>
    /// <param name="tripCheckIn">The Trip Check-In object to add.</param>
    /// <returns>The ID of the newly created Trip Check-In record.</returns>
    Task<int> InsertTripCheckInAsync(TripCheckin tripCheckIn);

    
    /// <summary>
    /// Updates an existing Trip Check-In record in the database.
    /// </summary>
    /// <param name="tripCheckIn">The Trip Check-In object with updated information.</param>
    /// <returns>True if the update was successful; otherwise, false.</returns>
    Task<bool> UpdateTripCheckInAsync(TripCheckin tripCheckIn);

    
    /// <summary>
    /// Deletes a Trip Check-In record by its unique ID.
    /// </summary>
    /// <param name="checkInId">The unique ID of the Trip Check-In to delete.</param>
    /// <returns>True if the deletion was successful; otherwise, false.</returns>
    Task<bool> DeleteTripCheckInAsync(int checkInId);

    //----------------------------------------------------------------------------------

    /// <summary>
    /// Retrieves a Trip Check-In record by its unique ID.
    /// </summary>
    /// <param name="checkInId">The unique ID of the Trip Check-In.</param>
    /// <returns>The Trip Check-In object with the specified ID, or null if not found.</returns>
    Task<TripCheckin?> GetTripCheckInByIdAsync(int checkInId);

    
    /// <summary>
    /// Retrieves all Trip Check-In records associated with a specific Trip ID.
    /// </summary>
    /// <param name="tripId">The ID of the Trip.</param>
    /// <returns>A list of Trip Check-In objects for the specified Trip ID.</returns>
    Task<IEnumerable<TripCheckin>> GetTripCheckInsByTripIdAsync(int tripId);
    
    
    /// <summary>
    /// Calculates the average delay for all check-ins associated with a specific Trip ID.
    /// </summary>
    /// <param name="tripId">The ID of the Trip.</param>
    /// <returns>The average delay in minutes.</returns>
    Task<double> CalculateAverageDelayForTripAsync(int tripId);
}
