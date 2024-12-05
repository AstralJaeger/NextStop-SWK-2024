using System.Net;
using NextStop.Domain;

namespace NextStop.ServiceInterface;

public interface IEndPointService
{
    public Task<IEnumerable<StopPoint>> GetAllAsync();
}