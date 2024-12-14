using Microsoft.AspNetCore.Mvc;
using NextStop.Api.DTOs;
using NextStop.Api.Mappers;
using NextStop.Domain;
using NextStop.Service.Interfaces;

namespace NextStop.Api.Controllers;

/// <summary>
/// API Controller for managing holidays.
/// Provides endpoints for creating, retrieving, updating, and deleting holidays.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class HolidayController : ControllerBase
{
    private readonly IHolidayService holidayService;
    
    //......................................................................

    /// <summary>
    /// Initializes a new instance of the <see cref="HolidayController"/> class.
    /// </summary>
    public HolidayController(IHolidayService holidayService)
    {
        this.holidayService = holidayService ?? throw new ArgumentNullException(nameof(holidayService));
    }
    
    //**********************************************************************************
    // CREATE-Methods
    //**********************************************************************************

    /// <summary>
    /// Inserts a new holiday into the system.
    /// </summary>
    /// <param name="holidayDto">The holiday data for creation.</param>
    /// <returns>The created holiday as a DTO with its unique ID.</returns>
    [HttpPost]
    [Produces("application/json", "text/plain")]
    public async Task<ActionResult<HolidayDto>> InsertHoliday(HolidayForCreationDto holidayDto)

    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        if (holidayDto.Id is not 0 && await holidayService.HolidayAlreadyExists(holidayDto.Id))
        {
            return Conflict(StatusInfo.HolidayAlreadyExists(holidayDto.Id));
            
        }
        
        var newHoliday = holidayDto.ToHoliday();
        await holidayService.InsertHolidayAsync(newHoliday);

        return CreatedAtAction(
            actionName: nameof(GetHolidayById), 
            routeValues: new { id = newHoliday.Id },
            value: newHoliday.ToHolidayDto()
        );

    }
    
    //**********************************************************************************
    //READ-Methods
    //**********************************************************************************

    /// <summary>
    /// Retrieves all holidays from the system.
    /// </summary>
    /// <returns>A collection of holidays as DTOs.</returns>
    [HttpGet]
    public async Task<ActionResult> GetAllHolidays()
    {
        var result = await holidayService.GetAllHolidaysAsync();
        return Ok(result.Select(r => r.ToHolidayDto()));
    }

    //......................................................................

    /// <summary>
    /// Retrieves a holiday by its unique ID.
    /// </summary>
    /// <param name="id">The ID of the holiday to retrieve.</param>
    /// <returns>The holiday as a DTO if found, or a 404 status if not found.</returns>
    [HttpGet("by-id/{id:int}")]
    public async Task<ActionResult> GetHolidayById(int id)
    {
        var result = await holidayService.GetHolidayByIdAsync(id);
        if (result is null)
        {
            return NotFound(StatusInfo.InvalidHolidayId(id));
        }
        return Ok(result.ToHolidayDto());
    }
    
    //......................................................................
    
    /// <summary>
    /// Retrieves holidays occurring in a specific year.
    /// </summary>
    /// <param name="year">The year for which holidays should be retrieved.</param>
    /// <returns>A collection of holidays in the specified year as DTOs.</returns>
    [HttpGet("by-year/{year:int}")]
    public async Task<ActionResult> GetHolidaysByYear(int year)
    {
        var result = await holidayService.GetHolidaysByYearAsync(year);
        if (!result.Any())
        {
            return NotFound(StatusInfo.InvalidYearForHolidays(year));
        }
        return Ok(result.Select(h => h.ToHolidayDto()));
    }
    
    //......................................................................

    /// <summary>
    /// Checks if a specific date is a holiday.
    /// </summary>
    /// <param name="date">The date to check.</param>
    /// <returns><c>true</c> if the date is a holiday; otherwise, <c>false</c>.</returns>
    [HttpGet("is-holiday/{date}")]
    [Produces("application/json", "text/plain")]
    public async Task<ActionResult<bool>> IsHoliday(string date)
    {
        try
        {
            var result = await holidayService.IsHolidayAsync(date);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    
    //**********************************************************************************
    //UPDATE-Methods
    //**********************************************************************************

    /// <summary>
    /// Updates an existing holiday in the system.
    /// </summary>
    /// <param name="holidayId">The ID of the holiday to update.</param>
    /// <param name="holidayDto">The updated holiday data.</param>
    /// <returns>A 204 No Content status if the update was successful, or a 404 status if
    /// the holiday was not found.</returns>
    [HttpPut("update/{holidayId:int}")]
    public async Task<ActionResult> UpdateHoliday(int holidayId, HolidayForUpdateDto holidayDto)
    {
        var existingHoliday = await holidayService.GetHolidayByIdAsync(holidayId);
        if (existingHoliday == null)
        {
            return NotFound(StatusInfo.InvalidHolidayId(holidayId));
        }

        holidayDto.UpdateHoliday(existingHoliday);
        
        await holidayService.UpdateHolidayAsync(existingHoliday);

        return NoContent();
    }

    //**********************************************************************************
    // DELETE-Methods
    //**********************************************************************************

    /// <summary>
    /// Deletes a holiday from the system by its unique ID.
    /// </summary>
    /// <param name="id">The ID of the holiday to delete.</param>
    /// <returns>A 204 No Content status if the deletion was successful, or a 404 status
    /// if the holiday was not found.</returns>
    [HttpDelete("delete/{id:int}")]
    public async Task<ActionResult> DeleteHoliday(int id)
    {
        if (await holidayService.DeleteHolidayAsync(id))
        {
            return NoContent();
        }
        return NotFound(StatusInfo.InvalidHolidayId(id));

    }

}