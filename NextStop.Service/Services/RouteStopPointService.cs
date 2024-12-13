using NextStop.Dal.Interface;
using NextStop.Domain;
using NextStop.Service.Interfaces;

namespace NextStop.Service.Services;

public class RouteStopPointService(IRouteStopPointDao routeStopPointDao, IStopPointDao stopPointDao)
    : IRouteStopPointService
{

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


    public async Task<IEnumerable<RouteStopPoint>> GetAllRouteStopPointsAsync()
    {
        return await await RunInLockAsync(() => { return routeStopPointDao.GetAllRouteStopPointAsync(); });
    }

    public async Task<RouteStopPoint?> GetRouteStopPointByIdAsync(int id)
    {
        return await await RunInLockAsync(() => { return routeStopPointDao.GetRouteStopPointByIdAsync(id); });
    }

    public async Task<IEnumerable<RouteStopPoint>> GetRouteStopPointsByRouteIdAsync(int routeId)
    {
        return await await RunInLockAsync(() => { return routeStopPointDao.GetStopPointsByRouteIdAsync(routeId); });
    }

    public async Task<IEnumerable<RouteStopPoint>> GetRouteStopPointsByRouteNameAsync(string routeName)
    {

        return await await RunInLockAsync(() =>
        {
            return routeStopPointDao.GetRouteStopPointsByRouteNameAsync(routeName);
        });
    }

    public async Task<RouteStopPoint?> GetRouteStopPointByArrivalTimeAsync(DateTime arrivalTime)
    {
        return await await RunInLockAsync(() =>
        {
            return routeStopPointDao.GetStopPointByArrivalTimeAsync(arrivalTime);
        });
    }

    public async Task<RouteStopPoint?> GetRouteStopPointByDepartureTimeAsync(DateTime departureTime)
    {
        return await await RunInLockAsync(() =>
        {
            return routeStopPointDao.GetRouteStopPointByDepartureTimeAsync(departureTime);
        });
    }


    public async Task InsertRouteStopPointAsync(RouteStopPoint routeStopPoint)
    {
        if (routeStopPoint == null)
        {
            throw new ArgumentNullException(nameof(routeStopPoint));
        }

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

    public async Task<bool> RouteStopPointAlreadyExists(int routeStopPointId)
    {
        return await await RunInLockAsync(async () =>
        {
            var existingRouteStopPoint = await routeStopPointDao.GetRouteStopPointByIdAsync(routeStopPointId);
            return existingRouteStopPoint != null;
        });
    }
    
    public async Task<bool> IsSameRouteForRouteStopPoints(string startStopPointName, string endStopPointName)
    {
        return await await RunInLockAsync(async () =>
        {
            if (string.IsNullOrWhiteSpace(startStopPointName) || string.IsNullOrWhiteSpace(endStopPointName))
            {
                throw new ArgumentException("Start and end StopPoint names must be provided.");
            }

            // Holen der StopPoint-IDs basierend auf den Namen
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
            
            // Überprüfen, ob beide StopPoints auf derselben Route liegen
            return await await RunInLockAsync(() => 
                routeStopPointDao.IsSameRouteForRouteStopPoints(startStopPoint.Id, endStopPoint.Id));
        });
    }

    // public async Task<IEnumerable<RouteStopPoint>> GetRouteBetweenStopPointsAsync(string startStopPointName,
    //     string endStopPointName)
    // {
    //     // Hole den Start- und Ziel-StopPoint
    //     var startStopPoint = await stopPointDao.GetStopPointByNameAsync(startStopPointName);
    //     var endStopPoint = await stopPointDao.GetStopPointByNameAsync(endStopPointName);
    //
    //     if (startStopPoint == null || endStopPoint == null)
    //     {
    //         throw new Exception("Start or End StopPoint not found.");
    //     }
    //
    //     // Hole alle Routen, die den Start-StopPoint anfahren
    //     var startRoutes = await routeStopPointDao.GetRoutesByStopPointIdAsync(startStopPoint.Id);
    //
    //     // Hole alle Routen, die den Ziel-StopPoint anfahren
    //     var endRoutes = await routeStopPointDao.GetRoutesByStopPointIdAsync(endStopPoint.Id);
    //
    //     // Prüfe auf direkte Verbindungen
    //     var directRoutes = startRoutes.Where(r => endRoutes.Any(er => er.RouteId == r.RouteId)).ToList();
    //
    //     // Suche Verbindungen mit Umstiegen
    //     var transferRoutes = new List<RouteStopPoint>();
    //     foreach (var startRoute in startRoutes)
    //     {
    //         // Hole StopPoints für jede Route
    //         var startRouteStopPoints = await routeStopPointDao.GetStopPointsByRouteIdAsync(startRoute.RouteId);
    //
    //         foreach (var transferStop in startRouteStopPoints)
    //         {
    //             if (transferStop.StopPointId != startStopPoint.Id && transferStop.StopPointId != endStopPoint.Id)
    //             {
    //                 // Prüfe, ob der Transfer-StopPoint eine Verbindung zum Ziel hat
    //                 var transferToEndRoutes =
    //                     await routeStopPointDao.GetRoutesByStopPointIdAsync(transferStop.StopPointId);
    //                 if (transferToEndRoutes.Any(r => endRoutes.Select(er => er.RouteId).Distinct().Contains(r.RouteId)))
    //                 {
    //                     transferRoutes.Add(transferStop);
    //                 }
    //             }
    //         }
    //     }
    //
    //     // Kombiniere direkte und indirekte Routen
    //     return directRoutes.Concat(transferRoutes);
    // }

}