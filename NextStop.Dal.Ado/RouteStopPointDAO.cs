using NextStop.Dal.Interface;
using NextStop.Domain;

namespace NextStop.Dal.Ado;

public class RouteStopPointDAO :IRouteStopPointDAO
{
    public Task InsertAsync(RouteStopPoint routeStopPoint)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(RouteStopPoint routeStopPoint)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<RouteStopPoint> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<RouteStopPoint>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<List<RouteStopPoint>> GetStopPointsByRouteIdAsync(int routeId)
    {
        throw new NotImplementedException();
    }

    public Task<RouteStopPoint?> GetNextStopPointAsync(int routeId, int currentStopOrder)
    {
        throw new NotImplementedException();
    }
}