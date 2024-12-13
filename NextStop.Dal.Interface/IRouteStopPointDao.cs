using System.Collections;
using NextStop.Domain;

namespace NextStop.Dal.Interface;

/// <summary>
/// Data Access Object interface for managing Route Stop Point-related operations.
/// </summary>
public interface IRouteStopPointDao
{

    /// <summary>
    /// Asynchronously inserts a new route stop point into the database.
    /// </summary>
    /// <param name="routeStopPoint">The route stop point object to insert.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task InsertRouteStopPointAsync(RouteStopPoint routeStopPoint);

    
    /// <summary>
    /// Asynchronously updates an existing route stop point in the database.
    /// </summary>
    /// <param name="routeStopPoint">The route stop point object with updated information.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task UpdateRouteStopPointAsync(RouteStopPoint routeStopPoint);

    
    /// <summary>
    /// Asynchronously deletes a route stop point by its unique ID.
    /// </summary>
    /// <param name="id">The unique ID of the route stop point to delete.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task DeleteRouteStopPointAsync(int id);
    
    //----------------------------------------------------------------------------------

    /// <summary>
    /// Asynchronously retrieves a route stop point by its unique ID.
    /// </summary>
    /// <param name="id">The unique ID of the route stop point.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing the route stop point with the specified ID.</returns>
    Task<RouteStopPoint?> GetRouteStopPointByIdAsync(int id);

    
    /// <summary>
    /// Asynchronously retrieves all route stop points from the database.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing a list of all route stop points.</returns>
    Task<IEnumerable<RouteStopPoint>> GetAllRouteStopPointAsync();


    /// <summary>
    /// Asynchronously retrieves all stop points for a specific route.
    /// </summary>
    /// <param name="routeId">The ID of the route.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing a list of stop points for the specified route.</returns>
    Task<IEnumerable<RouteStopPoint>> GetStopPointsByRouteIdAsync(int routeId);

    
    /// <summary>
    /// Asynchronously retrieves the next stop point for a specific route, based on the current stop order.
    /// </summary>
    /// <param name="routeId">The ID of the route.</param>
    /// <param name="currentStopOrder">The order of the current stop point.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing the next stop point if it exists, or null if it is the last stop.</returns>
    Task<RouteStopPoint?> GetNextRouteStopPointAsync(int routeId, int currentStopOrder);

    Task<IEnumerable<RouteStopPoint>> GetRouteBetweenStopPointsAsync(int startStopPointId, int endStopPointId);
    Task<IEnumerable<RouteStopPoint>> GetRouteStopPointsByRouteNameAsync(string routeName);
    Task<RouteStopPoint?> GetStopPointByArrivalTimeAsync(DateTime arrivalTime);
    Task<RouteStopPoint?> GetRouteStopPointByDepartureTimeAsync(DateTime arrivalTime);
    Task<IEnumerable<RouteStopPoint>> GetRoutesByStopPointIdAsync(int id);
    Task<bool> IsSameRouteForRouteStopPoints(int startId, int endId);
}
