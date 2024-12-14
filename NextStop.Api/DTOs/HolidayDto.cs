using System.ComponentModel.DataAnnotations;
using NextStop.Domain;
namespace NextStop.Api.DTOs;

/// <summary>
/// Data Transfer Object (DTO) for representing a holiday.
/// </summary>
public record HolidayDto
{
    /// <summary>
    /// Gets the unique ID of the holiday.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Gets or sets the name of the holiday.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the start date of the holiday.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date of the holiday.
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Gets or sets the type of the holiday.
    /// </summary>
    public HolidayType HolidayType { get; set; }

};


/// <summary>
/// DTO for creating a new holiday.
/// Includes validation attributes for required fields and business rules.
/// </summary>
public record HolidayForCreationDto
{
    /// <summary>
    /// Gets the unique ID of the holiday to be created.
    /// </summary>
    public int Id { get; init; }
    
    /// <summary>
    /// Gets or sets the name of the holiday.
    /// </summary>
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, ErrorMessage = "Name must be between 1 and 100 characters.")]
    public required string Name { get; set; } 
    
    /// <summary>
    /// Gets or sets the start date of the holiday.
    /// </summary>
    [Required(ErrorMessage = "StartDate is required.")]
    public required DateTime StartDate { get; set; }
    
    /// <summary>
    /// Gets or sets the end date of the holiday.
    /// Includes a custom validation to ensure it is greater than StartDate.
    /// </summary>
    [Required(ErrorMessage = "EndDate is required.")]
    [DateGreaterThan("StartDate", ErrorMessage = "EndDate must be later than StartDate.")]
    public required DateTime EndDate { get; set; }
    
    /// <summary>
    /// Gets or sets the type of the holiday.
    /// </summary>
    [Required(ErrorMessage = "HolidayType is required.")]
    public required HolidayType HolidayType { get; set; }


    /// <summary>
    /// Converts the DTO into a <see cref="Holiday"/> domain object.
    /// </summary>
    /// <returns>A <see cref="Holiday"/> object representing the DTO data.</returns>
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


/// <summary>
/// DTO for updating an existing holiday.
/// </summary>
public record HolidayForUpdateDto
{

    /// <summary>
    /// Gets or sets the updated name of the holiday.
    /// </summary>
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, ErrorMessage = "Name must be between 1 and 100 characters.")]
    public required string Name { get; set; } 
    
    /// <summary>
    /// Gets or sets the updated start date of the holiday.
    /// </summary>
    [Required(ErrorMessage = "StartDate is required.")]
    public required DateTime StartDate { get; set; }
    
    /// <summary>
    /// Gets or sets the updated end date of the holiday.
    /// </summary>
    [Required(ErrorMessage = "EndDate is required.")]
    [DateGreaterThan("StartDate", ErrorMessage = "EndDate must be later than StartDate.")]
    public required DateTime EndDate { get; set; }
    
    /// <summary>
    /// Gets or sets the updated type of the holiday.
    /// </summary>
    [Required(ErrorMessage = "HolidayType is required.")]
    public required HolidayType HolidayType { get; set; }
    
    /// <summary>
    /// Updates an existing <see cref="Holiday"/> object with the data from this DTO.
    /// </summary>
    /// <param name="holiday">The existing holiday object to update.</param>
    /// <exception cref="ArgumentNullException">Thrown if the holiday parameter is null.</exception>
    public void UpdateHoliday(Holiday? holiday)
    {
        
        if (holiday is null)
        {
            throw new ArgumentNullException(nameof(holiday));
        }
        
        holiday.Name = this.Name;
        holiday.StartDate = this.StartDate;
        holiday.EndDate = this.EndDate;
        holiday.Type = this.HolidayType;
    }
    
}


/// <summary>
/// Custom validation attribute to ensure a date is greater than another specified date.
/// </summary>
public class DateGreaterThanAttribute : ValidationAttribute
{
    private readonly string _comparisonProperty;

    /// <summary>
    /// Initializes a new instance of the <see cref="DateGreaterThanAttribute"/> class.
    /// </summary>
    /// <param name="comparisonProperty">The name of the property to compare against.</param>
    public DateGreaterThanAttribute(string comparisonProperty)
    {
        _comparisonProperty = comparisonProperty;
    }

    /// <summary>
    /// Validates that the value is greater than the specified comparison property value.
    /// </summary>
    /// <param name="value">The value being validated.</param>
    /// <param name="validationContext">The context of the validation.</param>
    /// <returns>A <see cref="ValidationResult"/> indicating whether the value is valid.</returns>
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