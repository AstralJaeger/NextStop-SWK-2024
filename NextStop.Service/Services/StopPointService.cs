using NextStop.Dal.Interface;
using NextStop.Domain;
using NextStop.Service.Interfaces;

namespace NextStop.Service;

/// <summary>
/// Service for managing stoppoint.
/// </summary>
public class StopPointService(IStopPointDao stopPointDao): IStopPointService
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
    public async Task InsertStopPointAsync(StopPoint stopPoint)
    {
        ArgumentNullException.ThrowIfNull(stopPoint);

        await DoInLockAsync(async () =>
        {
            try
            {
                await stopPointDao.InsertStopPointAsync(stopPoint);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not insert stoppoint: {e.Message}");
            }
        });
    }
    
    //**********************************************************************************
    //READ-Methods
    //**********************************************************************************

    /// <inheritdoc />
    public async Task<IEnumerable<StopPoint>> GetAllStopPointsAsync()
    {
        return await await RunInLockAsync(() =>
        {
            return stopPointDao.GetAllStopPointsAsync();
        });
    }

    //......................................................................

    /// <inheritdoc />
    public async Task<StopPoint?> GetStopPointByIdAsync(int id)
    {
        return await await RunInLockAsync(() =>
        { 
            return stopPointDao.GetStopPointByIdAsync(id);
        });
    }

    //......................................................................

    /// <inheritdoc />
    public async Task<StopPoint?> GetStopPointByNameAsync(string name)
    {
        return await await RunInLockAsync( () =>
        {
            return stopPointDao.GetStopPointByNameAsync(name);
        });
    }

    //......................................................................

    /// <inheritdoc />
    public async Task<StopPoint?> GetStopPointByShortNameAsync(string shortName)
    {
        return await await RunInLockAsync( () =>
        {
            return stopPointDao.GetStopPointByShortNameAsync(shortName);
        });
    }

    public async Task<IEnumerable<StopPoint>> GetStopPointByCoordinatesAsync(double longitude, double latitude, double radius)
    {
        return await await RunInLockAsync(() =>
        {
             return stopPointDao.GetStopPointByCoordinates(longitude, latitude, radius);
        });
    }

    //......................................................................

    /// <inheritdoc />
    public async Task<IEnumerable<Route>> GetRoutesByStopPointAsync(int stopPointId)
    {
        return await await RunInLockAsync( () =>
        {
            return stopPointDao.GetRoutesByStopPointAsync(stopPointId);
        });
    }

    //......................................................................

    /// <inheritdoc />
    public async Task<bool> StopPointAlreadyExists(int stopPointId)
    {
        return await await RunInLockAsync(async () =>
        {
            var existingStopPoint = await stopPointDao.GetStopPointByIdAsync(stopPointId);
            return existingStopPoint is not null;
        });
    }

    //**********************************************************************************
    //UPDATE-Methods
    //**********************************************************************************

    /// <inheritdoc />
    public async Task UpdateStopPointAsync(StopPoint? stopPoint)
    {
        ArgumentNullException.ThrowIfNull(stopPoint);

        await DoInLockAsync(async () =>
        {
            var existingStopPoint = await stopPointDao.GetStopPointByIdAsync(stopPoint.Id);
            if (existingStopPoint is null)
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
    

    //**********************************************************************************
    // DELETE-Methods
    //**********************************************************************************

    /// <inheritdoc />
    public async Task<bool> DeleteStopPointAsync(int id)
    {
        return await await RunInLockAsync(() =>
        {
            return stopPointDao.DeleteStopPointAsync(id);
        });
    }


}