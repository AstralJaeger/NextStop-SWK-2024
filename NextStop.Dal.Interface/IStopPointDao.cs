﻿using NextStop.Domain;

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
    Task<int> InsertStopPointAsync(StopPoint stopPoint);

    //**********************************************************************************
    //**********************************************************************************

    /// <summary>
    /// Asynchronously retrieves all stop points from the database.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing a list of all stop point objects.</returns>
    Task<IEnumerable<StopPoint>> GetAllStopPointsAsync();

    //----------------------------------------------------------------------------------
    
    /// <summary>
    /// Asynchronously retrieves a stop point by its unique ID.
    /// </summary>
    /// <param name="id">The unique ID of the stop point.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing the stop point object with the specified ID.</returns>
    Task<StopPoint?> GetStopPointByIdAsync(int id);

    //----------------------------------------------------------------------------------

    /// <summary>
    /// Asynchronously retrieves all routes that pass through a specific stop point.
    /// </summary>
    /// <param name="stopPointId">The ID of the stop point.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation, containing a list of routes that include the specified stop point.
    /// </returns>
    Task<IEnumerable<Route>> GetRoutesByStopPointAsync(int stopPointId);

    //----------------------------------------------------------------------------------
    
    /// <summary>
    /// Asynchronously retrieves a stop point by its name.
    /// </summary>
    /// <param name="name">The name of the stop point.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation, containing the stop point object with the specified name, or <c>null</c> if not found.
    /// </returns>
    Task<StopPoint?> GetStopPointByNameAsync(string name);
    
    //----------------------------------------------------------------------------------

    /// <summary>
    /// Asynchronously retrieves a stop point by its short name.
    /// </summary>
    /// <param name="shortName">The short name of the stop point.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation, containing the stop point object with the specified short name, or <c>null</c> if not found.
    /// </returns>
    Task<StopPoint?> GetStopPointByShortNameAsync(string shortName);


    //**********************************************************************************
    //**********************************************************************************

    /// <summary>
    /// Asynchronously updates an existing stop point in the database.
    /// </summary>
    /// <param name="stopPoint">The stop point object with updated information.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task<bool> UpdateStopPointAsync(StopPoint stopPoint);

    //**********************************************************************************
    //**********************************************************************************

    /// <summary>
    /// Asynchronously deletes a stop point from the database by its unique ID.
    /// </summary>
    /// <param name="id">The unique ID of the stop point to delete.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task<bool> DeleteStopPointAsync(int id);


    Task<IEnumerable<StopPoint>> GetStopPointByCoordinates(double longitude, double latitude, double radius);
}
