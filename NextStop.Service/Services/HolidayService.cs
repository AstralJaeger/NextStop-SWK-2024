using NextStop.Dal.Interface;
using NextStop.Domain;
using NextStop.Service.Interfaces;

namespace NextStop.Service.Services;

public class HolidayService(IHolidayDao holidayDao) : IHolidayService
{
    private readonly IHolidayDao holidayDao = holidayDao;
    
    public async Task<IEnumerable<Holiday>> GetAllHolidaysAsync()
    {
        IEnumerable<Holiday> holidays = await holidayDao.GetAllHolidaysAsync();
        return holidays;
    }

    public async Task<Holiday> GetHolidayByIdAsync(int id)
    {
        Holiday holiday = await holidayDao.GetHolidayByIdAsync(id);
        return holiday;
    }

    public async Task<IEnumerable<Holiday>> GetHolidaysByYearAsync(int year)
    {
        IEnumerable<Holiday> holidaysByYear = await holidayDao.GetHolidaysByYearAsync(year);
        return holidaysByYear;
    }

    public async Task<bool> DeleteHolidayAsync(int id)
    {
        bool holidayDeletedSuccessfully = await holidayDao.DeleteHolidayAsync(id);
        return holidayDeletedSuccessfully;
    }

    public Task<bool> HolidayAlreadyExist(Guid holidayDtoId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> InsertHolidayAsync(Holiday newHoliday)
    {
        throw new NotImplementedException();
    }
}