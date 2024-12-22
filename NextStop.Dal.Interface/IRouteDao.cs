using NextStop.Domain;

namespace NextStop.Dal.Interface;

/// <summary>
/// Data Access Object interface for managing Route-related operations.
/// </summary>
public interface IRouteDao
{
    
    /// <summary>
    ///  Inserts a new route into the database and retrieves the generated unique identifier (ID).
    /// </summary>
    /// <param name="route">The route object to insert.</param>
    /// <returns>
    /// A <see cref="Task{int}"/> representing the asynchronous operation, returning the unique identifier (ID) 
    /// of the newly created route.
    /// </returns>
    Task<int> InsertRouteAsync(Route route);
    
    //**********************************************************************************
    //**********************************************************************************
    
    /// <summary>
    /// Asynchronously retrieves all routes from the database.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing a list of all route objects.</returns>
    Task<IEnumerable<Route>> GetAllRoutesAsync();
    
    //----------------------------------------------------------------------------------
    
    /// <summary>
    /// Asynchronously retrieves a route by its unique ID.
    /// </summary>
    /// <param name="id">The unique ID of the route.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing the route object with the specified ID.</returns>
    Task<Route?> GetRouteByIdAsync(int id);
    
    //----------------------------------------------------------------------------------

    /// <summary>
    /// Asynchronously retrieves a route by its name.
    /// </summary>
    /// <param name="name">The name of the route.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation, containing the route object with the specified name or <c>null</c> if not found.
    /// </returns>
    Task<Route?> GetRouteByNameAsync(string name);
    
    //----------------------------------------------------------------------------------
    
    /// <summary>
    /// Asynchronously retrieves routes that are valid starting from a specific date.
    /// </summary>
    /// <param name="validFrom">The starting date for validity.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation, containing a collection of routes valid from the specified date.
    /// </returns>
    Task<IEnumerable<Route>> GetRouteByValidFromAsync(DateTime validFrom);
    
    //----------------------------------------------------------------------------------
    
    /// <summary>
    /// Asynchronously retrieves routes that are valid until a specific date.
    /// </summary>
    /// <param name="validTo">The ending date for validity.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation, containing a collection of routes valid until the specified date.
    /// </returns>
    Task<IEnumerable<Route>> GetRouteByValidToAsync(DateTime validTo);
    
    
    //Task<Task<IEnumerable<Route>>> GetRoutesByValidityDayAsync(int validityDay);
}