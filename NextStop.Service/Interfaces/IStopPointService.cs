using NextStop.Domain;

namespace NextStop.Service.Interfaces;

public interface IStopPointService
{
    public Task<IEnumerable<StopPoint>> GetAllStopPointsAsync();
    public Task<StopPoint> GetStopPointByIdAsync(int id);
}