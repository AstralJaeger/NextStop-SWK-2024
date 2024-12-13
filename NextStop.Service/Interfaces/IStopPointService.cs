using NextStop.Domain;

namespace NextStop.Service.Interfaces;

public interface IStopPointService
{
    public Task<IEnumerable<StopPoint>> GetAllStopPointsAsync();
    public Task<StopPoint> GetStopPointByIdAsync(int id);
    public Task<StopPoint?> GetStopPointByNameAsync(string name);
    public Task<StopPoint?> GetStopPointByShortNameAsync(string shortName);
    public Task<IEnumerable<Route>> GetRoutesByStopPointAsync(int stopPointId);
    public Task InsertStopPointAsync(StopPoint stopPoint);
    public Task<bool> StopPointAlreadyExists(int id);
    public Task<bool> DeleteStopPointAsync(int id);
    public Task UpdateStopPointAsync(StopPoint existingStopPoint);
}