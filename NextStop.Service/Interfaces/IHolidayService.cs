
using NextStop.Domain;
namespace NextStop.Service.Interfaces;

/// <summary>
/// Interface for managing holiday-related operations.
/// Provides methods for retrieving, inserting, updating, and deleting holiday data.
/// </summary>
public interface IHolidayService
{
    //**********************************************************************************
    // CREATE-Methods
    //**********************************************************************************

    /// <summary>
    /// Inserts a new holiday into the system.
    /// </summary>
    /// <param name="newHoliday">The <see cref="Holiday"/> object to insert.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task InsertHolidayAsync(Holiday newHoliday);
    
    //**********************************************************************************
    //READ-Methods
    //**********************************************************************************

    /// <summary>
    /// Retrieves all holidays from the system.
    /// </summary>
    /// <returns>A collection of all <see cref="Holiday"/> objects.</returns>
    public Task<IEnumerable<Holiday>> GetAllHolidaysAsync();
    
    //......................................................................

    /// <summary>
    /// Retrieves a specific holiday by its unique ID.
    /// </summary>
    /// <param name="id">The unique ID of the holiday.</param>
    /// <returns>The <see cref="Holiday"/> object if found; otherwise, <c>null</c>.</returns>
    public Task<Holiday?> GetHolidayByIdAsync(int id);

    //......................................................................

    /// <summary>
    /// Retrieves all holidays for a specified year.
    /// </summary>
    /// <param name="year">The year for which to retrieve holidays.</param>
    /// <returns>A collection of <see cref="Holiday"/> objects for the specified year.</returns>
    public Task<IEnumerable<Holiday>> GetHolidaysByYearAsync(int year);
    
    //......................................................................
  
    /// <summary>
    /// Checks if a holiday with the specified ID already exists.
    /// </summary>
    /// <param name="holidayId">The ID of the holiday to check.</param>
    /// <returns><c>true</c> if the holiday exists; otherwise, <c>false</c>.</returns>
    public Task<bool> HolidayAlreadyExists(int holidayId);

    //......................................................................

    /// <summary>
    /// Checks if a specific date falls within any holiday period.
    /// </summary>
    /// <param name="date">The date to check, in string format.</param>
    /// <returns><c>true</c> if the date is a holiday; otherwise, <c>false</c>.</returns>
    public Task<bool> IsHolidayAsync(string date);
    
    //**********************************************************************************
    //UPDATE-Methods
    //**********************************************************************************

    /// <summary>
    /// Updates an existing holiday with new information.
    /// </summary>
    /// <param name="holiday">The <see cref="Holiday"/> object containing updated information.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task UpdateHolidayAsync(Holiday? holiday);
    
    //**********************************************************************************
    // DELETE-Methods
    //**********************************************************************************

    /// <summary>
    /// Deletes a holiday by its unique ID.
    /// </summary>
    /// <param name="id">The unique ID of the holiday to delete.</param>
    /// <returns><c>true</c> if the holiday was successfully deleted; otherwise, <c>false</c>.</returns>
    public Task<bool> DeleteHolidayAsync(int id);
}