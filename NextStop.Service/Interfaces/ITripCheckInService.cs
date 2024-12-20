﻿using NextStop.Domain;

namespace NextStop.Service.Interfaces;

/// <summary>
/// Interface for managing trip check-in-related operations, including creation, retrieval, and updates of trip check-ins.
/// </summary>
public interface ITripCheckInService
{
    //**********************************************************************************
    // CREATE-Methods
    //**********************************************************************************

    /// <summary>
    /// Inserts a new trip check-in record into the system.
    /// </summary>
    /// <param name="checkIn">The <see cref="TripCheckin"/> object to insert.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task InsertTripCheckinAsync(TripCheckin tripCheckin);

    //**********************************************************************************
    // READ-Methods
    //**********************************************************************************

    /// <summary>
    /// Retrieves all trip check-ins from the system.
    /// </summary>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing
    /// a collection of <see cref="TripCheckin"/> objects.</returns>
    public Task<IEnumerable<TripCheckin>> GetAllTripCheckinsAsync();
    
    //......................................................................

    /// <summary>
    /// Retrieves a trip check-in record by its unique ID.
    /// </summary>
    /// <param name="id">The unique ID of the trip check-in.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing
    /// the <see cref="TripCheckin"/> object if found otherwise, <c>null</c>.</returns>
    public Task<TripCheckin?> GetTripCheckinByIdAsync(int id);

    //......................................................................

    /// <summary>
    /// Retrieves all trip check-ins associated with a specific trip ID.
    /// </summary>
    /// <param name="tripId">The ID of the trip.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing
    /// a collection of <see cref="TripCheckin"/> objects for the specified trip.</returns>
    public Task<IEnumerable<TripCheckin>> GetTripCheckinsByTripIdAsync(int tripId);

    //......................................................................

    /// <summary>
    /// Retrieves all trip check-ins associated with a specific stop point ID.
    /// </summary>
    /// <param name="stopPointId">The unique ID of the stop point.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing
    /// a collection of <see cref="TripCheckin"/> objects for the specified stop point.</returns>
    public Task<IEnumerable<TripCheckin>> GetTripCheckinsByStopPointIdAsync(int stopPointId);
    //......................................................................

    /// <summary>
    /// Retrieves all trip check-ins that match a specific check-in time.
    /// </summary>
    /// <param name="checkIn">The check-in time to search for.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation, containing
    /// a collection of <see cref="TripCheckin"/> objects with the specified check-in time.</returns>
    public Task<IEnumerable<TripCheckin>> GetTripCheckinsByCheckin(DateTime checkIn);

    /// <summary>
    /// Checks if a trip check-in with the specified ID already exists.
    /// </summary>
    /// <param name="tripChekinId">The ID of the trip check-in to check.</param>
    /// <returns><c>true</c> if the trip check-in exists; otherwise, <c>false</c>.</returns>
    Task<bool> TripCheckinAlreadyExists(int tripChekinId);
    
}