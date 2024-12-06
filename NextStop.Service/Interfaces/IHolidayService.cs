using NextStop.Domain;
namespace NextStop.Service.Interfaces;

public interface IHolidayService
{
    public Task<IEnumerable<Holiday>> GetAllHolidaysAsync();
    public Task<Holiday> GetHolidayByIdAsync(int id);
    public Task<IEnumerable<Holiday>> GetHolidaysByYearAsync(int year);
    public Task<bool> DeleteHolidayAsync(int id);
    public Task<bool> HolidayAlreadyExist(Guid holidayDtoId);
    public Task<bool> InsertHolidayAsync(Holiday newHoliday);
}