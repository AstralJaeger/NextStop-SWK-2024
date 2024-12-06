using Microsoft.AspNetCore.Mvc;
using NextStop.Api.DTOs;
using NextStop.Domain;
using NextStop.Service.Interfaces;

namespace NextStop.Api.Controllers;

[ApiController]
//[ProducesResponseType(StatusCodes.Status200OK)] 
[Route("api/[controller]")]
public class HolidayController(IHolidayService holidayService) : ControllerBase
{
    private readonly IHolidayService holidayService;
    
    [HttpGet]
    public async Task<ActionResult> GetHolidays()
    {
        var result = await holidayService.GetAllHolidaysAsync();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetHolidaysById(int id)
    {
        var result = await holidayService.GetHolidayByIdAsync(id);
        return Ok(result);
    }
    
    [HttpGet("{year:int}")]
    public async Task<ActionResult> GetHolidaysByYear(int year)
    {
        var result = await holidayService.GetHolidaysByYearAsync(year);
        return Ok(result);
    }
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteHoliday(int id)
    {
        var result = await holidayService.DeleteHolidayAsync(id);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<ActionResult<HolidayDto>> InsertHoliday(HolidayForCreationDto holidayDto)
    //string name, string startDate, string endDate, string type
    {
        if (holidayDto.Id != Guid.Empty && await holidayService.HolidayAlreadyExist(holidayDto.Id))
        {
            return BadRequest(); //todo something like this --> return Conflict(StatusInfo.CustomerAlreadyExists(customerDto.Id));
        }
        
        Holiday newHoliday = holidayDto.ToHoliday();
        await holidayService.InsertHolidayAsync(newHoliday);
        
        return CreatedAtAction(//todo)
        //do something like this
        
        // // Gibt 201 Created zurück und liefert die Kunden-Details.
        // return CreatedAtAction(
        //     actionName: nameof(GetCustomerById),  // Verweist auf den Endpunkt zum Abrufen eines Kunden.
        //     routeValues: new { customerId = customer.Id },
        //     value: customer.ToCustomerDto()
        // )
    }


    [HttpPut("{id:int}")]
    UpdateHolidayAsync
        
    [HttpGet("{id:int}")]
    IsHolidayAsync
    
}