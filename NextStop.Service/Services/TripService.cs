using NextStop.Dal.Interface;
using NextStop.Domain;
using NextStop.Service.Interfaces;

namespace NextStop.Service.Services;

public class TripService (ITripDao tripDao) : ITripService
{
    //Semaphore zur Sicherstellung von Thread-Sicherheit bei gleichzeitigen Zugriffen.
    private static readonly SemaphoreSlim semaphore = new(1, 1);
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

    // Führt eine Aktion thread-sicher aus.
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
    
    
    public async Task<IEnumerable<Trip>> GetAllTripsAsync()
    {
        return await await RunInLockAsync(() =>
        {
            return tripDao.GetAllTripsAsync();
        });
    }

    public async Task<Trip?> GetTripByIdAsync(int id)
    {
        return await await RunInLockAsync(() =>
        {
            return tripDao.GetTripByIdAsync(id);
        });
    }
    

    public async Task<IEnumerable<Trip>> GetTripsByRouteIdAsync(int routeId)
    {
        return await await RunInLockAsync(() =>
        {
            return tripDao.GetTripsByRouteIdAsync(routeId);
        });
    }

    public async Task<IEnumerable<Trip>> GetTripsByVehicleIdAsync(int vehicleId)
    {
        return await await RunInLockAsync(() =>
        {
            return tripDao.GetTripsByVehicleIdAsync(vehicleId);
        });
    }

    public async Task<bool> TripAlreadyExists(int id)
    {
        return await await RunInLockAsync(async () =>
        {
            var existingTrip = await tripDao.GetTripByIdAsync(id);
            return existingTrip != null;
        });
    }

    public async Task InsertTripAsync(Trip newTrip)
    {
        if (newTrip == null)
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
}