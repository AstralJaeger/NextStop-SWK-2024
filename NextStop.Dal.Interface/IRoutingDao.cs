using NextStop.Domain;

namespace NextStop.Dal.Interface;

public interface IRoutingDao
{
    public Task<IList<Connection>> GetConnectionAtTimeAsync(StopPoint start, StopPoint destination, DateTime time);
}