using NextStop.Domain;

namespace NextStop.Dal.Interface;

/// <summary>
/// Data Access Object interface for managing Route-related operations.
/// </summary>
public interface IRouteDao
{
    
    /// <summary>
    /// Asynchronously inserts a new route into the database.
    /// </summary>
    /// <param name="route">The route object to insert.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task<int> InsertRouteAsync(Route route);
    
    
    /// <summary>
    /// Asynchronously updates an existing route in the database.
    /// </summary>
    /// <param name="route">The route object with updated information.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task UpdateRouteAsync(Route route);
    
    
    /// <summary>
    /// Asynchronously deletes a route from the database by its unique ID.
    /// </summary>
    /// <param name="id">The unique ID of the route to delete.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task DeleteRouteAsync(int id);
    
    //----------------------------------------------------------------------------------

    /// <summary>
    /// Asynchronously retrieves a route by its unique ID.
    /// </summary>
    /// <param name="id">The unique ID of the route.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing the route object with the specified ID.</returns>
    Task<Route?> GetRouteByIdAsync(int id);
    
    
    /// <summary>
    /// Asynchronously retrieves all routes from the database.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing a list of all route objects.</returns>
    Task<IEnumerable<Route>> GetAllRoutesAsync();

    Task<Route> GetRouteByNameAsync(string name);
    
    
    Task<IEnumerable<Route>> GetRouteByValidFromAsync(DateTime validFrom);
    
    Task<IEnumerable<Route>> GetRouteByValidToAsync(DateTime validTo);
    
    
    //Task<Task<IEnumerable<Route>>> GetRoutesByValidityDayAsync(int validityDay);
}