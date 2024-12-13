using NextStop.Dal.Interface;
using NextStop.Domain;
using NextStop.Service.Interfaces;

namespace NextStop.Service;

public class StopPointService(IStopPointDao stopPointDao): IStopPointService
{
    //private readonly IStopPointDao stopPointDao = stopPointDao;
    
    //Semaphore zur Sicherstellung von Thread-Sicherheit bei gleichzeitigen Zugriffen.
    private static readonly SemaphoreSlim semaphore = new(1, 1);
    
    // Führt eine Funktion thread-sicher aus und gibt das Ergebnis zurück.
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

    
    public async Task<IEnumerable<StopPoint>> GetAllStopPointsAsync()
    {
        return await await RunInLockAsync(() =>
        {
            return stopPointDao.GetAllStopPointsAsync();
        });
    }

    public async Task<StopPoint?> GetStopPointByIdAsync(int id)
    {
        return await await RunInLockAsync(() =>
        { 
            return stopPointDao.GetStopPointByIdAsync(id);
        });
    }

    public async Task<StopPoint?> GetStopPointByNameAsync(string name)
    {
        return await await RunInLockAsync( () =>
        {
            return stopPointDao.GetStopPointByNameAsync(name);
        });
    }

    public async Task<StopPoint?> GetStopPointByShortNameAsync(string shortName)
    {
        return await await RunInLockAsync( () =>
        {
            return stopPointDao.GetStopPointByShortNameAsync(shortName);
        });
    }

    public async Task<IEnumerable<Route>> GetRoutesByStopPointAsync(int stopPointId)
    {
        return await await RunInLockAsync( () =>
        {
            return stopPointDao.GetRoutesByStopPointAsync(stopPointId);
        });
    }


    public async Task InsertStopPointAsync(StopPoint newStopPoint)
    {
        if (newStopPoint == null)
        {
            throw new ArgumentNullException(nameof(newStopPoint));
        }

        await DoInLockAsync(async () =>
        {
            try
            {
                await stopPointDao.InsertStopPointAsync(newStopPoint);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not insert stoppoint: {e.Message}");
            }
        });
    }

    public async Task<bool> StopPointAlreadyExists(int stopPointId)
    {
        return await await RunInLockAsync(async () =>
        {
            var existingStopPoint = await stopPointDao.GetStopPointByIdAsync(stopPointId);
            return existingStopPoint is not null;
        });
    }

    
    public async Task UpdateStopPointAsync(StopPoint? stopPoint)
    {
        if (stopPoint == null)
        {
            throw new ArgumentNullException(nameof(stopPoint));
        }

        await DoInLockAsync(async () =>
        {
            var existingStopPoint = await stopPointDao.GetStopPointByIdAsync(stopPoint.Id);
            if (existingStopPoint == null)
            {
                throw new ArgumentException($"Could not find stop point with id: {stopPoint.Id}");
            }

            existingStopPoint.Name = stopPoint.Name;
            existingStopPoint.ShortName = stopPoint.ShortName;
            existingStopPoint.Location = stopPoint.Location;
            existingStopPoint.StopPointRoutes = stopPoint.StopPointRoutes;
        });
        
        await stopPointDao.UpdateStopPointAsync(stopPoint);
    }
    
    
    public async Task<bool> DeleteStopPointAsync(int id)
    {
        return await await RunInLockAsync(() =>
        {
            return stopPointDao.DeleteStopPointAsync(id);
        });
    }


}