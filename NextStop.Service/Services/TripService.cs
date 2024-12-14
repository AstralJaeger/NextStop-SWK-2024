using NextStop.Dal.Interface;
using NextStop.Domain;
using NextStop.Service.Interfaces;

namespace NextStop.Service.Services;

/// <summary>
/// Service for managing trips.
/// </summary>
public class TripService (ITripDao tripDao) : ITripService
{
    /// <summary>
    /// Semaphore to ensure thread safety during concurrent access.
    /// </summary>
    private static readonly SemaphoreSlim semaphore = new(1, 1);
    
    //......................................................................

    /// <summary>
    /// Executes a function in a thread-safe manner and returns the result.
    /// </summary>
    /// <typeparam name="T">The return type of the function.</typeparam>
    /// <param name="func">The function to be executed.</param>
    /// <returns>The result of the function.</returns>
    private static async Task<T> RunInLockAsync<T>(Func<T> func)
    {
        await semaphore.WaitAsync();
        try
        {
            return func();
        }
        finally
        {
            semaphore.Release();
        }
    }

    //......................................................................

    /// <summary>
    /// Executes an action in a thread-safe manner.
    /// </summary>
    /// <param name="action">The action to be executed.</param>
    private static async Task DoInLockAsync(Action action)
    {
        await semaphore.WaitAsync();
        try
        {
            action();
        }
        finally
        {
            semaphore.Release();
        }
    }
    
    //**********************************************************************************
    // CREATE-Methods
    //**********************************************************************************

    /// <inheritdoc />
    public async Task InsertTripAsync(Trip newTrip)
    {
        if (newTrip is null)
        {
            throw new ArgumentNullException(nameof(newTrip));
        }
        
        if (await TripAlreadyExists(newTrip.Id))
        {
            throw new InvalidOperationException($"Trip with ID {newTrip.Id} already exists.");
        }

        await DoInLockAsync(async () =>
        {
            try
            {
                await tripDao.InsertTripAsync(newTrip);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not insert holiday: {e.Message}");
            }
        });
    }
    
    //**********************************************************************************
    //READ-Methods
    //**********************************************************************************

    /// <inheritdoc />
    public async Task<IEnumerable<Trip>> GetAllTripsAsync()
    {
        return await await RunInLockAsync(() =>
        {
            return tripDao.GetAllTripsAsync();
        });
    }

    //......................................................................

    /// <inheritdoc />
    public async Task<Trip?> GetTripByIdAsync(int id)
    {
        return await await RunInLockAsync(() =>
        {
            return tripDao.GetTripByIdAsync(id);
        });
    }
    
    //......................................................................

    /// <inheritdoc />
    public async Task<IEnumerable<Trip>> GetTripsByRouteIdAsync(int routeId)
    {
        return await await RunInLockAsync(() =>
        {
            return tripDao.GetTripsByRouteIdAsync(routeId);
        });
    }

    //......................................................................

    /// <inheritdoc />
    public async Task<IEnumerable<Trip>> GetTripsByVehicleIdAsync(int vehicleId)
    {
        return await await RunInLockAsync(() =>
        {
            return tripDao.GetTripsByVehicleIdAsync(vehicleId);
        });
    }

    //......................................................................

    /// <inheritdoc />
    public async Task<bool> TripAlreadyExists(int id)
    {
        return await await RunInLockAsync(async () =>
        {
            var existingTrip = await tripDao.GetTripByIdAsync(id);
            return existingTrip is not null;
        });
    }
    
}