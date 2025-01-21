using NextStop.Domain;

namespace NextStop.Service.Interfaces;

/// <summary>
/// Interface for managing stop point-related operations, including creation, retrieval, updates, and deletion of stop points.
/// </summary>
public interface IStopPointService
{
    
    //**********************************************************************************
    // CREATE-Methods
    //**********************************************************************************

    /// <summary>
    /// Inserts a new stop point into the system.
    /// </summary>
    /// <param name="stopPoint">The <see cref="StopPoint"/> object to insert.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task InsertStopPointAsync(StopPoint stopPoint);

    //**********************************************************************************
    // READ-Methods
    //**********************************************************************************

    /// <summary>
    /// Retrieves all stop points from the system.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation,
    /// containing a collection of <see cref="StopPoint"/> objects.</returns>
    public Task<IEnumerable<StopPoint>> GetAllStopPointsAsync();
    
    //......................................................................

    /// <summary>
    /// Retrieves a stop point by its unique ID.
    /// </summary>
    /// <param name="id">The unique ID of the stop point.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation,
    /// containing the <see cref="StopPoint"/> object if found; otherwise, <c>null</c>.</returns>
    public Task<StopPoint?> GetStopPointByIdAsync(int id);
    
    //......................................................................

    /// <summary>
    /// Retrieves a stop point by its name.
    /// </summary>
    /// <param name="name">The name of the stop point.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation,
    /// containing the <see cref="StopPoint"/> object if found; otherwise, <c>null</c>.</returns>
    public Task<StopPoint?> GetStopPointByNameAsync(string name);
    
    //......................................................................

    /// <summary>
    /// Retrieves a stop point by its short name.
    /// </summary>
    /// <param name="shortName">The short name of the stop point.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation,
    /// containing the <see cref="StopPoint"/> object if found; otherwise, <c>null</c>.</returns>
    public Task<StopPoint?> GetStopPointByShortNameAsync(string shortName);
    
    //......................................................................

    /// <summary>
    /// Retrieves all stop points within the radius of a location.
    /// </summary>
    /// <param name="longitude">The longitude</param>
    /// <param name="latitude">The latitude</param>
    /// <param name="radius">The radius in meters</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation,
    /// containing a collection of <see cref="StopPoint"/> objects.</returns>
    public Task<IEnumerable<StopPoint>> GetStopPointByCoordinatesAsync(double longitude, double latitude, double radius);
    
    //......................................................................

    /// <summary>
    /// Retrieves all routes associated with a specific stop point.
    /// </summary>
    /// <param name="stopPointId">The unique ID of the stop point.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation,
    /// containing a collection of <see cref="Route"/> objects.</returns>
    public Task<IEnumerable<Route>> GetRoutesByStopPointAsync(int stopPointId);
    
    //......................................................................

    /// <summary>
    /// Retrieves all stoppoints matching the query.
    /// </summary>
    /// <param name="stopPointId">The unique ID of the stop point.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation,
    /// containing a collection of <see cref="Route"/> objects.</returns>
    public Task<IEnumerable<StopPoint>> QueryStopPointAsync(string query);
    
    //......................................................................

    /// <summary>
    /// Checks if a stop point already exists in the system.
    /// </summary>
    /// <param name="id">The unique ID of the stop point to check.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing
    /// <c>true</c> if the stop point exists; otherwise, <c>false</c>.</returns>
    public Task<bool> StopPointAlreadyExists(int stopPointId);
    
    //**********************************************************************************
    // UPDATE-Methods
    //**********************************************************************************

    /// <summary>
    /// Deletes a stop point from the system by its unique ID.
    /// </summary>
    /// <param name="id">The unique ID of the stop point to delete.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing
    /// <c>true</c> if the deletion was successful; otherwise, <c>false</c>.</returns>
    public Task UpdateStopPointAsync(StopPoint? stopPoint);

    //**********************************************************************************
    // DELETE-Methods
    //**********************************************************************************
    
    /// <summary>
    /// Updates the details of an existing stop point.
    /// </summary>
    /// <param name="existingStopPoint">The <see cref="StopPoint"/> object containing the updated information.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task<bool> DeleteStopPointAsync(int id);
}