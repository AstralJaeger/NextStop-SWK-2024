using System.Globalization;
using NextStop.Dal.Interface;
using NextStop.Domain;
using NextStop.Service.Interfaces;

namespace NextStop.Service.Services;

/// <summary>
/// Service for managing holidays.
/// </summary>
public class HolidayService(IHolidayDao holidayDao) : IHolidayService
{
    /// <summary>
    /// Semaphore to ensure thread safety during concurrent access.
    /// </summary>
    private static readonly SemaphoreSlim semaphore = new(1, 1);
    
    //......................................................................

    /// <summary>
    /// Executes a function in a thread-safe manner and returns the result.
    /// </summary>
    /// <typeparam name="T">The return type of the function.</typeparam>
    /// <param name="func">The function to be executed.</param>
    /// <returns>The result of the function.</returns>
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

    //......................................................................

    /// <summary>
    /// Executes an action in a thread-safe manner.
    /// </summary>
    /// <param name="action">The action to be executed.</param>
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
    
    //**********************************************************************************
    // CREATE-Methods
    //**********************************************************************************

    /// <inheritdoc />
    public async Task InsertHolidayAsync(Holiday newHoliday)
    {
        ArgumentNullException.ThrowIfNull(newHoliday);
        
        if (await HolidayAlreadyExists(newHoliday.Id))
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
    
    //**********************************************************************************
    //READ-Methods
    //**********************************************************************************

    /// <inheritdoc />
    public async Task<IEnumerable<Holiday>> GetAllHolidaysAsync()
    {
        return await await RunInLockAsync( () =>
        {
            return holidayDao.GetAllHolidaysAsync();
        });
    }

    //......................................................................

    /// <inheritdoc />
    public async Task<Holiday?> GetHolidayByIdAsync(int id)
    {
        return await await RunInLockAsync(() =>
        { 
            return holidayDao.GetHolidayByIdAsync(id);
        });
        
    }

    //......................................................................

    /// <inheritdoc />
    public async Task<IEnumerable<Holiday>> GetHolidaysByYearAsync(int year)
    {
        return await await RunInLockAsync( () =>
        {
            return holidayDao.GetHolidaysByYearAsync(year) ;
        });

    }

    //......................................................................

    /// <inheritdoc />
    public async Task<bool> IsHolidayAsync(string date)
    {
       
        if (!DateTime.TryParseExact(date, 
                "dd-MM-yyyy",
                new CultureInfo("de-AT"),
                DateTimeStyles.None,out var parsedDate))
        {
            throw new ArgumentException("Invalid date format.", nameof(date));
        }
        
        return await await RunInLockAsync(() =>
        {
            return holidayDao.IsHolidayAsync(parsedDate);
        });
        
    }

    //......................................................................

    /// <inheritdoc />
    public async Task<bool> HolidayAlreadyExists(int holidayId)
    {
        return await await RunInLockAsync(async () =>
        {
            var existingHoliday = await holidayDao.GetHolidayByIdAsync(holidayId);
            return existingHoliday is not null;
        });
    }

    //**********************************************************************************
    //UPDATE-Methods
    //**********************************************************************************

    /// <inheritdoc />
    public async Task UpdateHolidayAsync(Holiday? holiday)
    {
        ArgumentNullException.ThrowIfNull(holiday);

        await DoInLockAsync(async () =>
        {
            var existingHoliday = await holidayDao.GetHolidayByIdAsync(holiday.Id);
            if (existingHoliday is null)
            {
                throw new InvalidOperationException($"Holiday with ID {holiday.Id} not found.");
            }
            
            existingHoliday.Name = holiday.Name;
            existingHoliday.StartDate = holiday.StartDate;
            existingHoliday.EndDate = holiday.EndDate;
            existingHoliday.Type = holiday.Type;
            
            await holidayDao.UpdateHolidayAsync(existingHoliday);
        });
    }
    
    //**********************************************************************************
    // DELETE-Methods
    //**********************************************************************************

    /// <inheritdoc />
    public async Task<bool> DeleteHolidayAsync(int id)
    {
        return await await RunInLockAsync(() =>
        {
            return holidayDao.DeleteHolidayAsync(id);
        });
    }

}