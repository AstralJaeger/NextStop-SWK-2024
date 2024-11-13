using NextStop.Dal.Interface;
using NextStop.Domain;

namespace NextStop.Dal.Ado;

public class RouteDao : IRouteDao
{
    public Task InsertAsync(Route route)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Route route)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Route> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Route>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}