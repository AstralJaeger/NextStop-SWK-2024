using NextStop.Domain;

namespace Routing;

public interface IRouteFinder
{
    public Task<IList<Connection>> FindConnection(StopPoint startId, StopPoint endId);
}