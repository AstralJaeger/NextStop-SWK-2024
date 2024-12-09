
using NextStop.Domain;
namespace NextStop.Service.Interfaces;

public interface IHolidayService
{
    public Task<IEnumerable<Holiday>> GetAllHolidaysAsync();
    public Task<Holiday?> GetHolidayByIdAsync(int id);
    public Task<IEnumerable<Holiday>> GetHolidaysByYearAsync(int year);
    public Task<bool> DeleteHolidayAsync(int id);
    public Task<bool> HolidayAlreadyExist(int holidayDtoId);
    public Task<bool> InsertHolidayAsync(Holiday newHoliday);
    public Task<bool> IsHolidayAsync(string date);
    public Task UpdateHolidayAsync(Holiday? holiday);
}