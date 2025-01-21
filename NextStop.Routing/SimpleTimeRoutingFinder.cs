using NextStop.Dal.Interface;
using NextStop.Domain;

namespace Routing;

public class SimpleTimeRoutingFinder(IRouteDao routeDao, IRouteStopPointDao routeStopPointDao, IStopPointDao stopPointDao)
    : BaseRouteFinder(routeDao, routeStopPointDao, stopPointDao)
{
    public DateTime Time { get; set; }

    /// <inheritdoc />
    public override async Task<IList<Connection>> FindConnection(StopPoint start, StopPoint destination)
    {
        RouteStopPoint? startPoint = null;
        RouteStopPoint? destinationPoint = null;
        var initialRoutes = await RouteDao.GetRouteByStopPointsAsync(start, Time);
        foreach (var route in initialRoutes)
        {
            if (!await HasDestination(destination, route)) continue;
            foreach (var rsp in await RouteStopPointDao.GetRouteStopPointsByRouteIdAsync(route.Id))
            {
                if (rsp.StopPointId == start.Id)
                {
                    startPoint = rsp;
                }
                if (rsp.StopPointId == destination.Id && startPoint != null && rsp.Order > startPoint.Order)
                {
                    destinationPoint = rsp;
                }
            }
        }

        if (startPoint == null || destinationPoint == null)
        {
            throw new RouteNotFoundException($"No direct route found from {start.Name} to {destination.Name}"); 
        }
        
        var connection = new Connection(Time, startPoint, destinationPoint);
        return (List<Connection>) [connection];
    }

    private async Task<bool> HasDestination(StopPoint destination, Route route)
    {
        var rtps = await RouteStopPointDao.GetRouteStopPointsByRouteIdAsync(route.Id);
        foreach (var rtp in  rtps)
        {
            if (rtp.StopPointId == destination.Id)
            {
                return true;
            }
        }

        return false;
    }
}