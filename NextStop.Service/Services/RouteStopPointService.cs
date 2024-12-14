using NextStop.Dal.Interface;
using NextStop.Domain;
using NextStop.Service.Interfaces;

namespace NextStop.Service.Services;

/// <summary>
/// Service for managing routestoppoints.
/// </summary>
public class RouteStopPointService(IRouteStopPointDao routeStopPointDao, IStopPointDao stopPointDao)
    : IRouteStopPointService
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
    public async Task InsertRouteStopPointAsync(RouteStopPoint routeStopPoint)
    {
        ArgumentNullException.ThrowIfNull(routeStopPoint);


        if (await RouteStopPointAlreadyExists(routeStopPoint.Id))
        {
            throw new InvalidOperationException($"RouteStopPoint with ID {routeStopPoint.Id} already exists.");
        }


        await DoInLockAsync(async () =>
        {
            try
            {
                await routeStopPointDao.InsertRouteStopPointAsync(routeStopPoint);
            }
            catch (Exception e)
            {

                Console.WriteLine($"Could not insert RouteStopPoint: {e.Message}");
            }
        });
    }
    
    //**********************************************************************************
    //READ-Methods
    //**********************************************************************************

    /// <inheritdoc />
    public async Task<IEnumerable<RouteStopPoint>> GetAllRouteStopPointsAsync()
    {
        return await await RunInLockAsync(() => { return routeStopPointDao.GetAllRouteStopPointAsync(); });
    }

    //......................................................................

    /// <inheritdoc />
    public async Task<RouteStopPoint?> GetRouteStopPointByIdAsync(int id)
    {
        return await await RunInLockAsync(() => { return routeStopPointDao.GetRouteStopPointByIdAsync(id); });
    }

    //......................................................................

    /// <inheritdoc />
    public async Task<IEnumerable<RouteStopPoint>> GetRouteStopPointsByRouteIdAsync(int routeId)
    {
        return await await RunInLockAsync(() => { return routeStopPointDao.GetStopPointsByRouteIdAsync(routeId); });
    }

    //......................................................................

    /// <inheritdoc />
    public async Task<IEnumerable<RouteStopPoint>> GetRouteStopPointsByRouteNameAsync(string routeName)
    {

        return await await RunInLockAsync(() =>
        {
            return routeStopPointDao.GetRouteStopPointsByRouteNameAsync(routeName);
        });
    }

    //......................................................................

    /// <inheritdoc />
    public async Task<IEnumerable<RouteStopPoint>> GetRouteStopPointsByArrivalTimeAsync(DateTime arrivalTime)
    {
        return await await RunInLockAsync(() =>
        {
            return routeStopPointDao.GetRouteStopPointsByArrivalTimeAsync(arrivalTime);
        });
    }

    //......................................................................

    /// <inheritdoc />
    public async Task<IEnumerable<RouteStopPoint>> GetRouteStopPointsByDepartureTimeAsync(DateTime departureTime)
    {
        return await await RunInLockAsync(() =>
        {
            return routeStopPointDao.GetRouteStopPointsByDepartureTimeAsync(departureTime);
        });
    }

    //......................................................................

    /// <inheritdoc />
   public async Task<bool> RouteStopPointAlreadyExists(int routeStopPointId)
    {
        return await await RunInLockAsync(async () =>
        {
            var existingRouteStopPoint = await routeStopPointDao.GetRouteStopPointByIdAsync(routeStopPointId);
            return existingRouteStopPoint is not null;
        });
    }
    
    //......................................................................

    /// <inheritdoc />
    public async Task<bool> IsSameRouteForRouteStopPoints(string startStopPointName, string endStopPointName)
    {
        return await await RunInLockAsync(async () =>
        {
            if (string.IsNullOrWhiteSpace(startStopPointName) || string.IsNullOrWhiteSpace(endStopPointName))
            {
                throw new ArgumentException("Start and end StopPoint names must be provided.");
            }
            
            var startStopPoint = await stopPointDao.GetStopPointByNameAsync(startStopPointName);
            if (startStopPoint == null)
            {
                throw new ArgumentException($"StopPoint with name '{startStopPointName}' not found.");
            }

            var endStopPoint = await stopPointDao.GetStopPointByNameAsync(endStopPointName);
            if (endStopPoint == null)
            {
                throw new ArgumentException($"StopPoint with name '{endStopPointName}' not found.");
            }
            
            return await await RunInLockAsync(() => 
                routeStopPointDao.IsSameRouteForRouteStopPoints(startStopPoint.Id, endStopPoint.Id));
        });
    }

   

}