using NextStop.Domain;
namespace NextStop.Api.DTOs;

public record HolidayDto
{
    public Guid Id { get; init; }

    public required string Name { get; set; }

    public required DateTime StartDate { get; set; }

    public required DateTime EndDate { get; set; }

    public required HolidayType HolidayType { get; set; }

};


public record HolidayForCreationDto
{
    public Guid Id { get; init; }
    
    public required string Name { get; set; } 
    
    public required DateTime StartDate { get; set; }
    
    public required DateTime EndDate { get; set; }
    
    public required HolidayType HolidayType { get; set; }

    public Holiday ToHoliday()
    {
        throw new NotImplementedException();
    }
}