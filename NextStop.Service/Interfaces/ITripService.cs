using NextStop.Domain;

namespace NextStop.Service.Interfaces;

/// <summary>
/// Interface for managing trip-related operations, including creation, retrieval, and validation of trips.
/// </summary>
public interface ITripService
{
    //**********************************************************************************
    // CREATE-Methods
    //**********************************************************************************
    
    /// <summary>
    /// Inserts a new trip record into the system.
    /// </summary>
    /// <param name="trip">The <see cref="Trip"/> object to insert.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task InsertTripAsync(Trip trip);
    //------------------------------------------------------------------------------

    //**********************************************************************************
    // READ-Methods
    //**********************************************************************************

    /// <summary>
    /// Retrieves all trips from the system.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing
    /// a collection of <see cref="Trip"/> objects.</returns>
    public Task<IEnumerable<Trip>> GetAllTripsAsync();

    //......................................................................

    /// <summary>
    /// Retrieves a trip by its unique ID.
    /// </summary>
    /// <param name="id">The unique ID of the trip.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing
    /// the <see cref="Trip"/> object if found, or null if not found.</returns>
    public Task<Trip?> GetTripByIdAsync(int id);

    //......................................................................

    /// <summary>
    /// Retrieves all trips associated with a specific route ID.
    /// </summary>
    /// <param name="routeId">The ID of the route.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing
    /// a collection of <see cref="Trip"/> objects for the specified route.</returns>
    public Task<IEnumerable<Trip>> GetTripsByRouteIdAsync(int routeId);

    //......................................................................

    /// <summary>
    /// Retrieves all trips associated with a specific vehicle ID.
    /// </summary>
    /// <param name="vehicleId">The ID of the vehicle.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing
    /// a collection of <see cref="Trip"/> objects for the specified vehicle.</returns>
    public Task<IEnumerable<Trip>> GetTripsByVehicleIdAsync(int vehicleId);

    //......................................................................

    /// <summary>
    /// Checks if a trip with the specified ID already exists in the system.
    /// </summary>
    /// <param name="id">The unique ID of the trip to check.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing
    /// true if the trip exists, otherwise false.</returns>
    public Task<bool> TripAlreadyExists(int id);
    
}