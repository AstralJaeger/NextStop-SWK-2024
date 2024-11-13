using NextStop.Dal.Interface;
using NextStop.Domain;

namespace NextStop.Dal.Ado;

public class HolidayDAO : IHolidayDAO
{
    public Task<int> InsertHolidayAsync(Holiday holiday)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateHolidayAsync(Holiday holiday)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteHolidayAsync(int holidayId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsHolidayAsync(DateTime date)
    {
        throw new NotImplementedException();
    }

    public Task<Holiday?> GetHolidayByIdAsync(int holidayId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Holiday>> GetAllHolidaysAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Holiday>> GetHolidaysByYearAsync(int year)
    {
        throw new NotImplementedException();
    }
}