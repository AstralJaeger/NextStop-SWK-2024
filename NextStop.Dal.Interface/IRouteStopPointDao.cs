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

    //**********************************************************************************
    //**********************************************************************************
    
    /// <summary>
    /// Asynchronously retrieves all route stop points from the database.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing a list of all route stop points.</returns>
    Task<IEnumerable<RouteStopPoint>> GetAllRouteStopPointAsync();
    
    //----------------------------------------------------------------------------------

    /// <summary>
    /// Asynchronously retrieves a route stop point by its unique ID.
    /// </summary>
    /// <param name="id">The unique ID of the route stop point.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing the route stop point with the specified ID.</returns>
    Task<RouteStopPoint?> GetRouteStopPointByIdAsync(int id);

    //----------------------------------------------------------------------------------

    /// <summary>
    /// Asynchronously retrieves all route stop points for a specific route.
    /// </summary>
    /// <param name="routeId">The ID of the route.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing a list of route stop points for the specified route.</returns>
    Task<IEnumerable<RouteStopPoint>> GetRouteStopPointsByRouteIdAsync(int routeId);

    //----------------------------------------------------------------------------------

    /// <summary>
    /// Asynchronously retrieves all route stop points for a specific route name.
    /// </summary>
    /// <param name="routeName">The name of the route.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation, containing a list of route stop points for the specified route name.
    /// </returns>
    Task<IEnumerable<RouteStopPoint>> GetRouteStopPointsByRouteNameAsync(string routeName);
    
    //----------------------------------------------------------------------------------

    /// <summary>
    /// Retrieves all route stop points that are valid on the specified days (binary-encoded format).
    /// </summary>
    /// <param name="validOn">
    /// The binary-encoded integer representing the days of the week when the route stop point is valid.
    /// For example:
    /// <list type="bullet">
    /// <item><description>1 (Sunday)</description></item>
    /// <item><description>62 (Monday to Friday)</description></item>
    /// <item><description>127 (Monday to Sunday)</description></item>
    /// </list>
    /// </param>
    /// <returns>
    /// A collection of <see cref="RouteStopPoint"/> objects that match the specified validity days.
    /// </returns>
    /// <remarks>
    /// This method queries the database for all route stop points where the `valid_on` column matches the provided value.
    /// Use binary encoding to represent day combinations (e.g., 62 for Monday to Friday, 127 for all days).
    /// </remarks>
    Task<IEnumerable<RouteStopPoint>> GetRouteStopPointByValidOnAsync(int validOn);

    
    //----------------------------------------------------------------------------------

    /// <summary>
    /// Asynchronously retrieves route stop points by its arrival time.
    /// </summary>
    /// <param name="arrivalTime">The arrival time of the stop point.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation, containing  a list of route stop points for the specified arrival time.
    /// </returns>
    Task<IEnumerable<RouteStopPoint>> GetRouteStopPointsByArrivalTimeAsync(DateTime arrivalTime);

    //----------------------------------------------------------------------------------

    /// <summary>
    /// Asynchronously retrieves route stop points by its departure time.
    /// </summary>
    /// <param name="departureTime">The departure time of the stop point.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation, containing a list of route stop points for the specified departure time.
    /// </returns>
    Task<IEnumerable<RouteStopPoint>> GetRouteStopPointsByDepartureTimeAsync(DateTime departureTime);

    //----------------------------------------------------------------------------------

    //todo 
    Task<RouteStopPoint?> GetNextRouteStopPointAsync(int routeId, int currentStopOrder);
    
    //----------------------------------------------------------------------------------

    /// <summary>
    /// Checks if two stop points belong to the same route.
    /// </summary>
    /// <param name="startId">The ID of the first stop point.</param>
    /// <param name="endId">The ID of the second stop point.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation, containing <c>true</c> if the stop points belong to the same route; otherwise, <c>false</c>.
    /// </returns>
    /// 
    Task<bool> IsSameRouteForRouteStopPoints(int startId, int endId);
    
    //----------------------------------------------------------------------------------

    
    Task<IEnumerable<RouteStopPoint>> GetRoutesByStopPointIdAsync(int stopPointId);

   
    //----------------------------------------------------------------------------------

    /// <summary>
    /// Retrieves the current delay for a specific RouteStopPoint.
    /// </summary>
    /// <param name="routeStopPointId">The ID of the RouteStopPoint.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation,
    /// containing the current delay in minutes for the specified RouteStopPoint.</returns>

    Task<int> GetCurrentDelayForRouteStopPoint(int routeStopPointId);
    
    //Task<IEnumerable<RouteStopPoint>> GetRouteBetweenStopPointsAsync(int startStopPointId, int endStopPointId);

    //----------------------------------------------------------------------------------

    //Task<RouteStopPoint?> GetConnectingStopPointAsync()

}
