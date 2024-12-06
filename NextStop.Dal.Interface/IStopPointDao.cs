using NextStop.Domain;

namespace NextStop.Dal.Interface;

/// <summary>
/// Data Access Object interface for managing StopPoint-related operations.
/// </summary>
public interface IStopPointDao
{
    /// <summary>
    /// Asynchronously inserts a new stop point into the database.
    /// </summary>
    /// <param name="stopPoint">The stop point object to insert.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task<int> InsertAsync(StopPoint stopPoint);


    /// <summary>
    /// Asynchronously updates an existing stop point in the database.
    /// </summary>
    /// <param name="stopPoint">The stop point object with updated information.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task<bool> UpdateAsync(StopPoint stopPoint);


    /// <summary>
    /// Asynchronously deletes a stop point from the database by its unique ID.
    /// </summary>
    /// <param name="id">The unique ID of the stop point to delete.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task<bool> DeleteAsync(int id);
    
    //----------------------------------------------------------------------------------

    /// <summary>
    /// Asynchronously retrieves a stop point by its unique ID.
    /// </summary>
    /// <param name="id">The unique ID of the stop point.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing the stop point object with the specified ID.</returns>
    Task<StopPoint?> GetByIdAsync(int id);

    
    /// <summary>
    /// Asynchronously retrieves all stop points from the database.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing a list of all stop point objects.</returns>
    Task<IEnumerable<StopPoint>> GetAllStopPointsAsync();

}