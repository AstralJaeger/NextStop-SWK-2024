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
    
    //**********************************************************************************
    //**********************************************************************************

    
    /// <summary>
    /// Retrieves all trips from the database.
    /// </summary>
    /// <returns>A list of all trip objects.</returns>
    Task<IEnumerable<Trip>> GetAllTripsAsync();

    //----------------------------------------------------------------------------------

    /// <summary>
    /// Retrieves a trip by its unique ID.
    /// </summary>
    /// <param name="tripId">The unique ID of the trip.</param>
    /// <returns>The trip object with the specified ID, or null if not found.</returns>
    Task<Trip?> GetTripByIdAsync(int tripId);

    //----------------------------------------------------------------------------------

    /// <summary>
    /// Retrieves all trips associated with a specific route ID.
    /// </summary>
    /// <param name="routeId">The ID of the route.</param>
    /// <returns>A list of trip objects for the specified route.</returns>
    Task<IEnumerable<Trip>> GetTripsByRouteIdAsync(int routeId);

    //----------------------------------------------------------------------------------

    /// <summary>
    /// Retrieves all trips associated with a specific vehicle ID.
    /// </summary>
    /// <param name="vehicleId">The ID of the vehicle.</param>
    /// <returns>A list of trip objects for the specified vehicle.</returns>
    Task<IEnumerable<Trip>> GetTripsByVehicleIdAsync(int vehicleId);

    //----------------------------------------------------------------------------------

 
}
