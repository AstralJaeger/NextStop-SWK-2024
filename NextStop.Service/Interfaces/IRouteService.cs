using NextStop.Domain;

namespace NextStop.Service.Interfaces;

/// <summary>
/// Interface for managing route-related operations.
/// Provides methods for creating, retrieving, and checking routes.
/// </summary>
public interface IRouteService
{
    //**********************************************************************************
    // CREATE-Methods
    //**********************************************************************************

    /// <summary>
    ///  Inserts a new route into the database and retrieves the generated unique identifier (ID).
    /// </summary>
    /// <param name="route">The <see cref="Route"/> object to insert.</param>
    /// <returns>
    /// A <see cref="Task{int}"/> representing the asynchronous operation, returning the unique identifier (ID) 
    /// of the newly created route.
    /// </returns>
    Task<int> InsertRouteAsync(Route route);
    
    //**********************************************************************************
    // READ-Methods
    //**********************************************************************************

    /// <summary>
    /// Retrieves all routes from the system.
    /// </summary>
    /// <returns>A collection of all <see cref="Route"/> objects.</returns>

    public Task<IEnumerable<Route>> GetAllRoutesAsync();

    //......................................................................
    
    /// <summary>
    /// Retrieves a route by its unique ID.
    /// </summary>
    /// <param name="id">The unique ID of the route.</param>
    /// <returns>The <see cref="Route"/> object if found; otherwise, <c>null</c>.</returns>
    public Task<Route?> GetRouteByIdAsync(int id);
    
    //......................................................................

    /// <summary>
    /// Retrieves a route by its unique name.
    /// </summary>
    /// <param name="name">The name of the route.</param>
    /// <returns>The <see cref="Route"/> object if found; otherwise, <c>null</c>.</returns>
    public Task<Route?> GetRouteByNameAsync(string name);

    
    //......................................................................
   
    /// <summary>
    /// Retrieves all routes valid until a specific date.
    /// </summary>
    /// <param name="validToDate">The date until which the routes should be valid.</param>
    /// <returns>A collection of <see cref="Route"/> objects valid until the specified date.</returns>
    public Task<IEnumerable<Route>> GetRoutesByValidToAsync(DateTime validToDate);

    
    //......................................................................

    /// <summary>
    /// Retrieves all routes valid from a specific date onward.
    /// </summary>
    /// <param name="validFromDate">The date from which the routes should be valid.</param>
    /// <returns>A collection of <see cref="Route"/> objects valid from the specified date.</returns>
    public Task<IEnumerable<Route>> GetRoutesByValidFromAsync(DateTime validFromDate);

    
    //......................................................................

    /// <summary>
    /// Checks if a route with the specified ID already exists in the system.
    /// </summary>
    /// <param name="routeId">The ID of the route to check.</param>
    /// <returns><c>true</c> if the route exists; otherwise, <c>false</c>.</returns>
    public Task<bool> RouteAlreadyExist(int routeId);
    
    //public Task<IEnumerable<Route>> GetRoutesByValidityDayAsync(int v);
    //GetDelayStatisticsAsync
}