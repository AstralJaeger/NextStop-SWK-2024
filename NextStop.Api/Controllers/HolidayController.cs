using Microsoft.AspNetCore.Mvc;
using NextStop.Api.DTOs;
using NextStop.Api.Mappers;
using NextStop.Domain;
using NextStop.Service.Interfaces;

namespace NextStop.Api.Controllers;

[ApiController]
//[ProducesResponseType(StatusCodes.Status200OK)] 
[Route("api/[controller]")]
public class HolidayController : ControllerBase
{
    private readonly IHolidayService holidayService;
    
    // Klassischer Konstruktor für Dependency Injection
    public HolidayController(IHolidayService holidayService)
    {
        this.holidayService = holidayService ?? throw new ArgumentNullException(nameof(holidayService));
    }
    
    //todo Fehlerbehandlung
    
    [HttpGet]
    public async Task<ActionResult> GetAllHolidays()
    {
        var result = await holidayService.GetAllHolidaysAsync();
        return Ok(result.Select(r => r.ToHolidayDto()));
    }

    [HttpGet("by-id/{id:int}")]
    public async Task<ActionResult> GetHolidayById(int id)
    {
        var result = await holidayService.GetHolidayByIdAsync(id);
        if (result is null)
        {
            // Gibt 404 zurück, wenn der Kunde nicht existiert.
            return NotFound(StatusInfo.InvalidHolidayId(id));
        }
        return Ok(result.ToHolidayDto());
    }
    
    
    [HttpGet("by-year/{year:int}")]
    public async Task<ActionResult> GetHolidaysByYear(int year)
    {
        var result = await holidayService.GetHolidaysByYearAsync(year);
        if (result is null)
        {
            // Gibt 404 zurück, wenn der Kunde nicht existiert.
            return NotFound(StatusInfo.InvalidYearForHolidays(year));
        }
        return Ok(result);
    }
    
    
    [HttpGet("is-holiday/{date}")]
    public async Task<bool> IsHoliday(string date)
    {
        var result = await holidayService.IsHolidayAsync(date);
        return result;
    }
    
    
    

    [HttpPost]
    [Produces("application/json", "text/plain")]
    public async Task<ActionResult<HolidayDto>> InsertHoliday(HolidayForCreationDto holidayDto)

    {
        // Prüfe, ob das ModelState gültig ist
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // Gibt die Validierungsfehler zurück
        }
        
        if (holidayDto.Id != 0 && await holidayService.HolidayAlreadyExist(holidayDto.Id))
        {
            return Conflict(StatusInfo.HolidayAlreadyExists(holidayDto.Id));
            
        }
        
        Holiday newHoliday = holidayDto.ToHoliday();
        await holidayService.InsertHolidayAsync(newHoliday);

        return CreatedAtAction(
            actionName: nameof(GetHolidayById), // Verweist auf den Endpunkt zum Abrufen eines Kunden.
            routeValues: new { holidayId = newHoliday.Id },
            value: newHoliday.ToHolidayDto()
            );

    }


    [HttpPut("update/{holidayId:int}")]
    public async Task<ActionResult> UpdateHoliday(int holidayId, HolidayForUpdateDto holidayDto)
    {
        var existingHoliday = await holidayService.GetHolidayByIdAsync(holidayId);
        if (existingHoliday == null)
        {
            return NotFound();
        }

        holidayDto.UpdateHoliday(existingHoliday);
        
        await holidayService.UpdateHolidayAsync(existingHoliday);

        return NoContent();
    }

    
    [HttpDelete("delete/{id:int}")]
    public async Task<ActionResult> DeleteHoliday(int id)
    {
        if (await holidayService.DeleteHolidayAsync(id))
        {
            // Gibt 204 No Content zurück, wenn erfolgreich gelöscht.
            return NoContent();
        }
        else
        {
            
            return NotFound();
        }

    }

}