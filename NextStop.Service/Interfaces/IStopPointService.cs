using NextStop.Domain;

namespace NextStop.Service.Interfaces;

public interface IStopPointService
{
    public Task<IEnumerable<StopPoint>> GetAllAsync();
    public Task<StopPoint> GetStopPointByIdAsync(int id);
}