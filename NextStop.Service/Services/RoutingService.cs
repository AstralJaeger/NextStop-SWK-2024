using NextStop.Dal.Ado;
using NextStop.Dal.Interface;
using NextStop.Domain;
using Routing;

namespace NextStop.Service.Services;

public class RoutingService(IRouteDao routeDao, IRouteStopPointDao routeStopPointDao, IStopPointDao stopPointDao) : IRoutingService
{
    /// <summary>
    /// Semaphore to ensure thread safety during concurrent access.
    /// </summary>
    private static readonly SemaphoreSlim semaphore = new(1, 1);

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

    public async Task<IList<Connection>> GetConnectionAtTimeAsync(int startId, int destinationId, DateTime time)
    {
        return await await RunInLockAsync(async () =>
        {
            var startPoint = await stopPointDao.GetStopPointByIdAsync(startId);
            if (startPoint == null)
            {
                throw new ArgumentException($"StopPoint for start with ID {startId} not found.");
            }

            var destinationPoint = await stopPointDao.GetStopPointByIdAsync(destinationId);
            if (destinationPoint == null)
            {
                throw new ArgumentException($"StopPoint for start with ID {startId} not found.");
            }

            var finder = new SimpleTimeRoutingFinder(routeDao, routeStopPointDao, stopPointDao);
            finder.Time = time;
            return await finder.FindConnection(startPoint, destinationPoint);
        });
    }
}