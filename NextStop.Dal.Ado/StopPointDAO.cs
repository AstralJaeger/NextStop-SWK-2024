using NextStop.Dal.Interface;
using NextStop.Domain;

namespace NextStop.Dal.Ado;

public class StopPointDAO: IStopPointDAO
{
    public Task InsertAsync(StopPoint stopPoint)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(StopPoint stopPoint)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<StopPoint> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<StopPoint>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}