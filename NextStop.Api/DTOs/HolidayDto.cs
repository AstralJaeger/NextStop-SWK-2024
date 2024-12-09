using System.ComponentModel.DataAnnotations;
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
    
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, ErrorMessage = "Name must be between 1 and 100 characters.")]
    public required string Name { get; set; } 
    
    [Required]
    public required DateTime StartDate { get; set; }
    
    [Required]
    [DateGreaterThan("StartDate", ErrorMessage = "EndDate must be later than StartDate.")]
    public required DateTime EndDate { get; set; }
    
    [Required]
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



public class DateGreaterThanAttribute : ValidationAttribute
{
    private readonly string _comparisonProperty;

    public DateGreaterThanAttribute(string comparisonProperty)
    {
        _comparisonProperty = comparisonProperty;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var comparisonProperty = validationContext.ObjectType.GetProperty(_comparisonProperty);

        if (comparisonProperty == null)
        {
            return new ValidationResult($"Property '{_comparisonProperty}' not found.");
        }

        var comparisonValue = (DateTime)comparisonProperty.GetValue(validationContext.ObjectInstance);

        if ((DateTime)value <= comparisonValue)
        {
            return new ValidationResult(ErrorMessage ?? $"Date must be greater than {_comparisonProperty}.");
        }

        return ValidationResult.Success;
    }
}