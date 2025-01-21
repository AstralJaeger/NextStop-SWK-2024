using NextStop.Dal.Interface;
using NextStop.Domain;

namespace Routing;

/// <summary>
///  Base class for routing algorithms.
/// </summary>
/// <param name="routeDao">RouteDao Datasource</param>
/// <param name="routeStopPointDao">RouteStopPoint Datasource</param>
/// <param name="stopPointDao">StopPoint Datasource</param>
public abstract class BaseRouteFinder(
    IRouteDao routeDao,
    IRouteStopPointDao routeStopPointDao,
    IStopPointDao stopPointDao)
    : IRouteFinder
{
    protected readonly IRouteDao RouteDao = routeDao;
    protected readonly IRouteStopPointDao RouteStopPointDao = routeStopPointDao;
    protected readonly IStopPointDao StopPointDao = stopPointDao;

    /// <summary>
    /// Basic interface for routing algorithms.
    /// </summary>
    /// <param name="start">Source for routing algorithm</param>
    /// <param name="destination">Target for routing algorithm</param>
    /// <returns></returns>
    public abstract Task<IList<Connection>> FindConnection(StopPoint start, StopPoint destination);
}