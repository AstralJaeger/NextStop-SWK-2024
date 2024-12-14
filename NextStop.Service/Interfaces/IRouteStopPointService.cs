using NextStop.Domain;

namespace NextStop.Service.Interfaces;


/// <summary>
/// Interface for managing route stop point-related operations.
/// Provides methods for creating, retrieving, and validating route stop points.
/// </summary>
public interface IRouteStopPointService
{
    //**********************************************************************************
    // CREATE-Methods
    //**********************************************************************************
    
    /// <summary>
    /// Inserts a new route stop point into the system.
    /// </summary>
    /// <param name="routeStopPoint">The <see cref="RouteStopPoint"/> object to insert.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task InsertRouteStopPointAsync(RouteStopPoint routeStopPoint);

    //**********************************************************************************
    // READ-Methods
    //**********************************************************************************

    /// <summary>
    /// Retrieves all route stop points from the system.
    /// </summary>
    /// <returns>A collection of all <see cref="RouteStopPoint"/> objects.</returns>
    public Task<IEnumerable<RouteStopPoint>> GetAllRouteStopPointsAsync();
    
    //......................................................................

    /// <summary>
    /// Retrieves a route stop point by its unique ID.
    /// </summary>
    /// <param name="id">The unique ID of the route stop point.</param>
    /// <returns>The <see cref="RouteStopPoint"/> object if found; otherwise, <c>null</c>.</returns>
    public Task<RouteStopPoint?> GetRouteStopPointByIdAsync(int id);
    
    //......................................................................

    /// <summary>
    /// Retrieves all route stop points associated with a specific route ID.
    /// </summary>
    /// <param name="routeId">The ID of the route.</param>
    /// <returns>A collection of <see cref="RouteStopPoint"/> objects for the specified route.</returns>
    public Task<IEnumerable<RouteStopPoint>> GetRouteStopPointsByRouteIdAsync(int routeId);
    
    //......................................................................

    /// <summary>
    /// Retrieves all route stop points associated with a specific route name.
    /// </summary>
    /// <param name="routeName">The name of the route.</param>
    /// <returns>A collection of <see cref="RouteStopPoint"/> objects for the specified route name.</returns>
    public Task<IEnumerable<RouteStopPoint>> GetRouteStopPointsByRouteNameAsync(string routeName);

    //......................................................................

    /// <summary>
    /// Retrieves route stop points based on the arrival time.
    /// </summary>
    /// <param name="arrivalTime">The arrival time to search for.</param>
    /// <returns>A collection of <see cref="RouteStopPoint"/> objects for the specified arrival time.</returns>
    public Task<IEnumerable<RouteStopPoint>> GetRouteStopPointsByArrivalTimeAsync(DateTime arrivalTime);
    
    //......................................................................

    /// <summary>
    /// Retrieves route stop points based on the departure time.
    /// </summary>
    /// <param name="departureTime">The departure time to search for.</param>
    /// <returns>A collection of <see cref="RouteStopPoint"/> objects for the specified departure time.</returns>
    public Task<IEnumerable<RouteStopPoint>> GetRouteStopPointsByDepartureTimeAsync(DateTime departureTime);
    
    //......................................................................

    /// <summary>
    /// Checks if a route stop point already exists in the system.
    /// </summary>
    /// <param name="routeStopPointDto">The ID of the route stop point to check.</param>
    /// <returns><c>true</c> if the route stop point exists; otherwise, <c>false</c>.</returns>
    Task<bool> RouteStopPointAlreadyExists(int routeStopPointId);

    //......................................................................

    /// <summary>
    /// Checks if two stop points belong to the same route.
    /// </summary>
    /// <param name="startStopPointName">The name of the starting stop point.</param>
    /// <param name="endStopPointName">The name of the ending stop point.</param>
    /// <returns><c>true</c> if the stop points belong to the same route; otherwise, <c>false</c>.</returns>
    Task<bool> IsSameRouteForRouteStopPoints(string startStopPointName, string endStopPointName);
    
    //public Task<IEnumerable<RouteStopPoint>> GetRouteStopPointsByStopPointAsync(int stopPointId);
    // Task<IEnumerable<RouteStopPoint>> GetRouteBetweenStopPointsAsync(string startStopPointName, string endStopPointNam);

}