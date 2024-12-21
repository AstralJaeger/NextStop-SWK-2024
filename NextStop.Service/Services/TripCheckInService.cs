using NextStop.Common;
using NextStop.Dal.Ado;
using NextStop.Domain;
using NextStop.Service.Interfaces;

namespace NextStop.Service.Services;

/// <summary>
/// Service for managing tripcheckins.
/// </summary>
public class TripCheckInService(ITripCheckinDao tripCheckinDao) : ITripCheckInService
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
    public async Task InsertTripCheckinAsync(TripCheckin tripCheckin)
    {
        ArgumentNullException.ThrowIfNull(tripCheckin);

        if (await TripCheckinAlreadyExists(tripCheckin.Id))
        {
            throw new InvalidOperationException($"TripCheckin with ID {tripCheckin.Id} already exists.");

        }

        // Schritt 1: RouteID basierend auf TripID abrufen
        var routeId = await tripCheckinDao.GetRouteIdByTripIdAsync(tripCheckin.TripId);

        // Schritt 2: Geplante Ankunftszeit basierend auf RouteID und StopPointID abrufen
        var plannedArrivalTime = await tripCheckinDao.GetArrivalTimeByRouteAndStopPointAsync(routeId, tripCheckin.StopPointId);

        // Schritt 3: Verspätung in Minuten berechnen
        tripCheckin.Delay = (int)(tripCheckin.CheckIn - plannedArrivalTime).TotalMinutes;
        
        await DoInLockAsync(async () =>
        {
            try
            {
                await tripCheckinDao.InsertTripCheckinAsync(tripCheckin);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not insert TripCheckin: {e.Message}");
            
            }
        });
    }
    
    //**********************************************************************************
    //READ-Methods
    //**********************************************************************************

    /// <inheritdoc />
    public async Task<IEnumerable<TripCheckin>> GetAllTripCheckinsAsync()
    {
        return await await RunInLockAsync(() =>
        {
            return tripCheckinDao.GetAllTripCheckinsAsync();
        });
    }

    //......................................................................

    /// <inheritdoc />
    public async Task<TripCheckin?> GetTripCheckinByIdAsync(int id)
    {
        return await await RunInLockAsync(() =>
        {
            return tripCheckinDao.GetTripCheckinByIdAsync(id);
        });
    }

    //......................................................................

    /// <inheritdoc />
    public async Task<IEnumerable<TripCheckin>> GetTripCheckinsByCheckin(DateTime checkIn)
    {
        return await await RunInLockAsync(() =>
        {
            return tripCheckinDao.GetTripCheckinsByCheckin(checkIn);
        });
    }

    //......................................................................

    /// <inheritdoc />
    public async Task<bool> TripCheckinAlreadyExists(int tripChekinId)
    {
        return await await RunInLockAsync(async () =>
        {
            var existingTripCheckin = await tripCheckinDao.GetTripCheckinByIdAsync(tripChekinId);
            return existingTripCheckin is not null;
        });
    }

    //......................................................................

    /// <inheritdoc />
    public async Task<IEnumerable<TripCheckin>> GetTripCheckinsByTripIdAsync(int tripId)
    {
        return await await RunInLockAsync(() =>
        {
            return tripCheckinDao.GetTripCheckinsByTripIdAsync(tripId);
        });
    }

    //......................................................................

    /// <inheritdoc />
    public async Task<IEnumerable<TripCheckin>> GetTripCheckinsByStopPointIdAsync(int stopPointId)
    {
        return await await RunInLockAsync(() =>
        {
            return tripCheckinDao.GetTripCheckinsByStopPointIdAsync(stopPointId);
        });
    }
}