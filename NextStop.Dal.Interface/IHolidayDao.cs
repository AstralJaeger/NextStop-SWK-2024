using NextStop.Domain;

namespace NextStop.Dal.Interface;

/// <summary>
/// Data Access Object interface for handling holiday-related operations.
/// </summary>
public interface IHolidayDao
{
    /// <summary>
    /// Adds a new holiday to the database.
    /// </summary>
    /// <param name="holiday">The holiday object to add.</param>
    /// <returns>The ID of the newly created holiday record.</returns>
    Task<int> InsertHolidayAsync(Holiday holiday);


    /// <summary>
    /// Updates an existing holiday in the database.
    /// </summary>
    /// <param name="holiday">The holiday object with updated information.</param>
    /// <returns>True if the update was successful; otherwise, false.</returns>
    Task<bool> UpdateHolidayAsync(Holiday holiday);

    
    /// <summary>
    /// Deletes a holiday by its unique ID.
    /// </summary>
    /// <param name="holidayId">The unique ID of the holiday to delete.</param>
    /// <returns>True if the deletion was successful; otherwise, false.</returns>
    Task<bool> DeleteHolidayAsync(int holidayId);
    
    //----------------------------------------------------------------------------------


    /// <summary>
    /// Checks if a specific date is a holiday.
    /// </summary>
    /// <param name="date">The date to check.</param>
    /// <returns>True if the date is a holiday; otherwise, false.</returns>
    Task<bool> IsHolidayAsync(DateTime date);
    
    /// <summary>
    /// Retrieves a holiday by its unique ID.
    /// </summary>
    /// <param name="holidayId">The unique ID of the holiday.</param>
    /// <returns>The holiday object with the specified ID, or null if not found.</returns>
    Task<Holiday?> GetHolidayByIdAsync(int holidayId);

    /// <summary>
    /// Retrieves all holidays in the database.
    /// </summary>
    /// <returns>A list of all holiday objects.</returns>
    Task<IEnumerable<Holiday>> GetAllHolidaysAsync();

    /// <summary>
    /// Retrieves all holidays for a specific year.
    /// </summary>
    /// <param name="year">The year for which to retrieve holidays.</param>
    /// <returns>A list of holiday objects for the specified year.</returns>
    Task<IEnumerable<Holiday>> GetHolidaysByYearAsync(int year);
}
