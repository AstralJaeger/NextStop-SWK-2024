using System.Collections;
using NextStop.Dal.Interface;
using NextStop.Domain;
using NextStop.Service.Interfaces;

namespace NextStop.Service.Services;

public class RouteService(IRouteDao routeDao) : IRouteService
{
    //private readonly IRouteDao routeDao = routeDao;
    
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
    
    
    public async Task<IEnumerable<Route>> GetAllRoutesAsync()
    {
        return await await RunInLockAsync(() =>
        { 
            return routeDao.GetAllRoutesAsync();
        });
    }

    public async Task<Route?> GetRouteByIdAsync(int id)
    {
        return await await RunInLockAsync(() => 
        { 
            return routeDao.GetRouteByIdAsync(id);
        });
    }

    public async Task<Route> GetRouteByNameAsync(string name)
    {
        return await await RunInLockAsync(() => 
        { 
            return routeDao.GetRouteByNameAsync(name);
        });
    }

    public async Task<IEnumerable<Route>> GetRoutesByValidToAsync(DateTime validToDate)
    {
        return await await RunInLockAsync(() =>
        {
            return routeDao.GetRouteByValidToAsync(validToDate);
        });
    }

    public async Task<IEnumerable<Route>> GetRoutesByValidFromAsync(DateTime validFromDate)
    {
        return await await RunInLockAsync(() =>
        {
            return routeDao.GetRouteByValidFromAsync(validFromDate);
        });
    }

    public async Task InsertRouteAsync(Route route)
    {
        if (route == null)
        {
            throw new ArgumentNullException(nameof(route));
        }

        await DoInLockAsync(async () =>
        {
            try
            {
                await routeDao.InsertRouteAsync(route);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Could not insert route: {e.Message}");
            }
        });
    }

    
    public async Task<bool> RouteAlreadyExist(int routeId)
    {
        return await await RunInLockAsync(async () =>
        {
            var existingRoute = await routeDao.GetRouteByIdAsync(routeId);
            return existingRoute != null;
        });
    }
    


    // public async Task<IEnumerable<Route>> GetRoutesByValidityDayAsync(int validityDay)
    // {
    //     return await RunInLockAsync(() => 
    //     { 
    //         return routeDao.GetRoutesByValidityDayAsync(validityDay);
    //     });
    // }
}