using NextStop.Common;
using NextStop.Dal.Interface;
using NextStop.Domain;

namespace NextStop.Dal.Ado;

public class RoutingDao(IRouteDao routeDao, IRouteStopPointDao routeStopPointDao, IStopPointDao stopPointDao) : IRoutingDao
{
    public Task<IList<Connection>> GetConnectionAtTimeAsync(StopPoint start, StopPoint destination, DateTime time)
    {
        var finder = new SimpleTimeRoutingFinder(routeDao, routeStopPointDao, stopPointDao);
        finder.Time = time;
        return finder.FindConnection(start, destination);
    }
}