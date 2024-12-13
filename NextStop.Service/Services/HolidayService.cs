using NextStop.Dal.Interface;
using NextStop.Domain;
using NextStop.Service.Interfaces;

namespace NextStop.Service.Services;

public class HolidayService(IHolidayDao holidayDao) : IHolidayService
{
    //Semaphore zur Sicherstellung von Thread-Sicherheit bei gleichzeitigen Zugriffen.
    private static readonly SemaphoreSlim semaphore = new(1, 1);
    
    // Führt eine Funktion thread-sicher aus und gibt das Ergebnis zurück.
    private static async Task<T> RunInLockAsync<T>(Func<T> func)
    {
        await semaphore.WaitAsync();
        try
        {
            return func();
        }
        finally
        {
            semaphore.Release();
        }
    }

    // Führt eine Aktion thread-sicher aus.
    private static async Task DoInLockAsync(Action action)
    {
        await semaphore.WaitAsync();
        try
        {
            action();
        }
        finally
        {
            semaphore.Release();
        }
    }
    
    
    
    public async Task<IEnumerable<Holiday>> GetAllHolidaysAsync()
    {
        return await await RunInLockAsync(() =>
        {
            return holidayDao.GetAllHolidaysAsync();
        });
    }

    public async Task<Holiday?> GetHolidayByIdAsync(int id)
    {
        return await await RunInLockAsync(() =>
        { 
            return holidayDao.GetHolidayByIdAsync(id);
        });
        
    }

    public async Task<IEnumerable<Holiday>> GetHolidaysByYearAsync(int year)
    {
        return await await RunInLockAsync( () =>
        {
            return holidayDao.GetHolidaysByYearAsync(year);
        });

    }

    public async Task<bool> IsHolidayAsync(string date)
    {
       
        if (!DateTime.TryParse(date, out var parsedDate))
        {
            throw new ArgumentException("Invalid date format.", nameof(date));
        }
        
        return await await RunInLockAsync(() =>
        {
            return holidayDao.IsHolidayAsync(parsedDate);
        });
        
    }

    public async Task<bool> HolidayAlreadyExist(int holidayId)
    {
        return await await RunInLockAsync(async () =>
        {
            var existingHoliday = await holidayDao.GetHolidayByIdAsync(holidayId);
            return existingHoliday != null;
        });
    }



    public async Task InsertHolidayAsync(Holiday newHoliday)
    {
        if (newHoliday == null)
        {
            throw new ArgumentNullException(nameof(newHoliday));
        }
        
        if (await HolidayAlreadyExist(newHoliday.Id))
        {
            throw new InvalidOperationException($"Holiday with ID {newHoliday.Id} already exists.");
        }

        await DoInLockAsync(async () =>
        {
            try
            {
                await holidayDao.InsertHolidayAsync(newHoliday); 
            }
            catch (Exception e)
            {
     
                Console.WriteLine($"Could not insert holiday: {e.Message}");
            }
        });
    }



    public async Task UpdateHolidayAsync(Holiday? holiday)
    {
        if (holiday == null)
        {
            throw new ArgumentNullException(nameof(holiday));
        }

        await DoInLockAsync(async () =>
        {
            var existingHoliday = await holidayDao.GetHolidayByIdAsync(holiday.Id);
            if (existingHoliday == null)
            {
                throw new Exception($"Holiday with ID {holiday.Id} not found.");
            }

            // Aktualisiere die Felder im gespeicherten Holiday-Objekt
            existingHoliday.Name = holiday.Name;
            existingHoliday.StartDate = holiday.StartDate;
            existingHoliday.EndDate = holiday.EndDate;
            existingHoliday.Type = holiday.Type;

            // Speichere die Änderungen
            await holidayDao.UpdateHolidayAsync(existingHoliday);
        });
    }
    
    
    public async Task<bool> DeleteHolidayAsync(int id)
    {
        return await await RunInLockAsync(() =>
        {
            return holidayDao.DeleteHolidayAsync(id);
        });
    }

}