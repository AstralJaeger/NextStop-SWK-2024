using NextStop.Domain;

namespace NextStop.Dal.Interface;

/// <summary>
/// Data Access Object interface for managing Route-related operations.
/// </summary>
public interface IRouteDAO
{
    
    /// <summary>
    /// Asynchronously inserts a new route into the database.
    /// </summary>
    /// <param name="route">The route object to insert.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task InsertAsync(Route route);
    
    
    /// <summary>
    /// Asynchronously updates an existing route in the database.
    /// </summary>
    /// <param name="route">The route object with updated information.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task UpdateAsync(Route route);
    
    
    /// <summary>
    /// Asynchronously deletes a route from the database by its unique ID.
    /// </summary>
    /// <param name="id">The unique ID of the route to delete.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task DeleteAsync(int id);
    
    //----------------------------------------------------------------------------------

    /// <summary>
    /// Asynchronously retrieves a route by its unique ID.
    /// </summary>
    /// <param name="id">The unique ID of the route.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing the route object with the specified ID.</returns>
    Task<Route> GetByIdAsync(int id);
    
    
    /// <summary>
    /// Asynchronously retrieves all routes from the database.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing a list of all route objects.</returns>
    Task<List<Route>> GetAllAsync();
}