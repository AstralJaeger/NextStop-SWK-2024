using NextStop.Domain;

namespace NextStop.Dal.Interface;

/// <summary>
/// Data Access Object interface for handling Trip-related operations.
/// </summary>
public interface ITripDao
{
    /// <summary>
    /// Adds a new trip to the database.
    /// </summary>
    /// <param name="trip">The trip object to add.</param>
    /// <returns>The ID of the newly created trip record.</returns>
    Task<int> InsertTripAsync(Trip trip);

    
    /// <summary>
    /// Updates an existing trip in the database.
    /// </summary>
    /// <param name="trip">The trip object with updated information.</param>
    /// <returns>True if the update was successful; otherwise, false.</returns>
    Task<bool> UpdateTripAsync(Trip trip);

    
    /// <summary>
    /// Deletes a trip by its unique ID.
    /// </summary>
    /// <param name="tripId">The unique ID of the trip to delete.</param>
    /// <returns>True if the deletion was successful; otherwise, false.</returns>
    Task<bool> DeleteTripAsync(int tripId);

//----------------------------------------------------------------------------------

    /// <summary>
    /// Retrieves a trip by its unique ID.
    /// </summary>
    /// <param name="tripId">The unique ID of the trip.</param>
    /// <returns>The trip object with the specified ID, or null if not found.</returns>
    Task<Trip?> GetTripByIdAsync(int tripId);

    /// <summary>
    /// Retrieves all trips associated with a specific route ID.
    /// </summary>
    /// <param name="routeId">The ID of the route.</param>
    /// <returns>A list of trip objects for the specified route.</returns>
    Task<IEnumerable<Trip>> GetTripsByRouteIdAsync(int routeId);
    
    Task<IEnumerable<Trip>> GetTripsByVehicleIdAsync(int vehicleId);

    
    /// <summary>
    /// Retrieves all trips from the database.
    /// </summary>
    /// <returns>A list of all trip objects.</returns>
    Task<IEnumerable<Trip>> GetAllTripsAsync();
    
    
    /// <summary>
    /// Retrieves all trips scheduled for a specific date.
    /// </summary>
    /// <param name="date">The date for which to retrieve trips.</param>
    /// <returns>A list of trip objects scheduled on the specified date.</returns>
    //Task<IEnumerable<Trip>> GetTripsByDateAsync(DateTime date);

    
    /// <summary>
    /// Retrieves all ongoing trips at the current date and time.
    /// </summary>
    /// <returns>A list of trip objects that are currently ongoing.</returns>
    //Task<IEnumerable<Trip>> GetOngoingTripsAsync();

    

}
