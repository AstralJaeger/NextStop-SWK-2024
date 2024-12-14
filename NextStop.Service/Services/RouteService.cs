using System.Collections;
using NextStop.Dal.Interface;
using NextStop.Domain;
using NextStop.Service.Interfaces;

namespace NextStop.Service.Services;

/// <summary>
/// Service for managing routes.
/// </summary>
public class RouteService(IRouteDao routeDao) : IRouteService
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
    public async Task InsertRouteAsync(Route route)
    {
        if (route is null)
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
    
    //**********************************************************************************
    //READ-Methods
    //**********************************************************************************

    /// <inheritdoc />
    public async Task<IEnumerable<Route>> GetAllRoutesAsync()
    {
        return await await RunInLockAsync(() =>
        { 
            return routeDao.GetAllRoutesAsync();
        });
    }

    //......................................................................

    /// <inheritdoc />
    public async Task<Route?> GetRouteByIdAsync(int id)
    {
        return await await RunInLockAsync(() => 
        { 
            return routeDao.GetRouteByIdAsync(id);
        });
    }

    //......................................................................

    /// <inheritdoc />
    public async Task<Route?> GetRouteByNameAsync(string name)
    {
        return await await RunInLockAsync(() => 
        { 
            return routeDao.GetRouteByNameAsync(name);
        });
    }

    //......................................................................

    /// <inheritdoc />
    public async Task<IEnumerable<Route>> GetRoutesByValidToAsync(DateTime validToDate)
    {
        return await await RunInLockAsync(() =>
        {
            return routeDao.GetRouteByValidToAsync(validToDate);
        });
    }

    //......................................................................

    /// <inheritdoc />
    public async Task<IEnumerable<Route>> GetRoutesByValidFromAsync(DateTime validFromDate)
    {
        return await await RunInLockAsync(() =>
        {
            return routeDao.GetRouteByValidFromAsync(validFromDate);
        });
    }

    //......................................................................
    
    /// <inheritdoc />
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