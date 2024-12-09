using NextStop.Domain;
namespace NextStop.Api.DTOs;

public record HolidayDto
{
    public int Id { get; init; }

    public string Name { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public HolidayType HolidayType { get; set; }

};


// brauchen wir das separat??
public record HolidayForCreationDto
{
    public int Id { get; init; }
    
    public required string Name { get; set; } 
    
    public required DateTime StartDate { get; set; }
    
    public required DateTime EndDate { get; set; }
    
    public required HolidayType HolidayType { get; set; }


    public Holiday ToHoliday()
    {
        return new Holiday
        {
            Id = this.Id,
            Name = this.Name,
            StartDate = this.StartDate,
            EndDate = this.EndDate,
            Type = this.HolidayType
        };
    }
}

public record HolidayForUpdateDto
{
    //public int Id { get; init; }
    
    public required string Name { get; set; } 
    
    public required DateTime StartDate { get; set; }
    
    public required DateTime EndDate { get; set; }
    
    public required HolidayType HolidayType { get; set; }
    
    
    public void UpdateHoliday(Holiday? holiday)
    {
        
        if (holiday == null)
        {
            throw new ArgumentNullException(nameof(holiday));
        }
        
        holiday.Name = this.Name;
        holiday.StartDate = this.StartDate;
        holiday.EndDate = this.EndDate;
        holiday.Type = this.HolidayType;
    }
    
}