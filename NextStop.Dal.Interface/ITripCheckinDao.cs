﻿using NextStop.Domain;

namespace NextStop.Common;

/// <summary>
/// Data Access Object interface for handling Trip Check-In operations.
/// </summary>
public interface ITripCheckinDao
{


    /// <summary>
    /// Adds a new Trip Check-In record to the database.
    /// </summary>
    /// <param name="tripCheckIn">The Trip Check-In object to add.</param>
    /// <returns>The ID of the newly created Trip Check-In record.</returns>
    Task<int> InsertTripCheckinAsync(TripCheckin tripCheckIn);

    //**********************************************************************************
    //**********************************************************************************

    /// <summary>
    /// Retrieves all Trip Check-In records from the database.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation,
    /// containing a list of all Trip Check-In objects.</returns>
    Task<IEnumerable<TripCheckin>> GetAllTripCheckinsAsync();

    //----------------------------------------------------------------------------------

    /// <summary>
    /// Retrieves a Trip Check-In record by its unique ID.
    /// </summary>
    /// <param name="checkInId">The unique ID of the Trip Check-In.</param>
    /// <returns>The Trip Check-In object with the specified ID, or null if not found.</returns>
    Task<TripCheckin?> GetTripCheckinByIdAsync(int checkInId);

    //----------------------------------------------------------------------------------

    /// <summary>
    /// Retrieves all Trip Check-In records associated with a specific Trip ID.
    /// </summary>
    /// <param name="tripId">The ID of the Trip.</param>
    /// <returns>A list of Trip Check-In objects for the specified Trip ID.</returns>
    Task<IEnumerable<TripCheckin>> GetTripCheckinsByTripIdAsync(int tripId);

    //----------------------------------------------------------------------------------

    /// <summary>
    /// Retrieves all Trip Check-In records associated with a specific Stop Point ID.
    /// </summary>
    /// <param name="stopPointId">The ID of the Stop Point.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing
    /// a list of Trip Check-In objects for the specified Stop Point ID.</returns>
    Task<IEnumerable<TripCheckin>> GetTripCheckinsByStopPointIdAsync(int stopPointId);

    //----------------------------------------------------------------------------------

    /// <summary>
    /// Retrieves all Trip Check-In records for a specific check-in timestamp.
    /// </summary>
    /// <param name="checkIn">The check-in timestamp to filter by.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing
    /// a list of Trip Check-In objects with the specified check-in timestamp.</returns>
    Task<IEnumerable<TripCheckin>> GetTripCheckinsByCheckin(DateTime checkIn);

    //----------------------------------------------------------------------------------

    //todo
    Task<double> GetAverageDelayForTripAsync(int tripId);
}
