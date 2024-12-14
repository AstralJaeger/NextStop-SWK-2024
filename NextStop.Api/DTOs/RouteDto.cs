using System.ComponentModel.DataAnnotations;
using NextStop.Domain;
using Route = NextStop.Domain.Route;

namespace NextStop.Api.DTOs;

/// <summary>
/// Data Transfer Object (DTO) for representing a route.
/// </summary>
public record RouteDto
{
    /// <summary>
    /// Gets or initializes the unique ID of the route.
    /// </summary>
    public int Id { get; init; }
    
    /// <summary>
    /// Gets or sets the name of the route.
    /// </summary>
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, ErrorMessage = "Name must be between 1 and 100 characters.")]
    public string Name { get; set; }
    
    /// <summary>
    /// Gets or sets the starting validity date of the route.
    /// </summary>
    [Required(ErrorMessage = "ValidFrom is required.")]
    [DataType(DataType.Date, ErrorMessage = "ValidFrom must be a valid date.")]
    public DateTime ValidFrom { get; set; }
    
    /// <summary>
    /// Gets or sets the ending validity date of the route.
    /// </summary>
    [Required(ErrorMessage = "ValidTo is required.")]
    [DataType(DataType.Date, ErrorMessage = "ValidTo must be a valid date.")]
    [DateGreaterThan("ValidFrom", ErrorMessage = "ValidTo must be greater than ValidFrom.")]
    public DateTime ValidTo { get; set; }
    
    /// <summary>
    /// Gets or sets the binary-encoded days of the week on which the route is valid.
    /// Represented as an integer, where each bit corresponds to a day (1 = Monday, 7 = Sunday).
    /// </summary>
    [Required(ErrorMessage = "ValidOn is required.")]
    [BinaryDayOfWeek(ErrorMessage = "ValidOn must be a valid binary encoding for days of the week (1-127).")]
    public int ValidOn { get; set; }
    
    
}


/// <summary>
/// Custom validation attribute to ensure ValidOn is a valid binary encoding for days of the week.
/// </summary>
public class BinaryDayOfWeekAttribute : ValidationAttribute
{
    /// <summary>
    /// Validates that the value is a valid binary encoding for days of the week.
    /// </summary>
    /// <param name="value">The value being validated.</param>
    /// <returns><c>true</c> if the value is valid; otherwise, <c>false</c>.</returns>
    public override bool IsValid(object value)
    {
        if (value is not int validOn)
        {
            return false;
        }

        // Valid values: binary encoding from 1 (0000001) to 127 (1111111)
        return validOn >= 1 && validOn <= 127;
    }
}

/// <summary>
/// DTO for creating a new route.
/// Includes methods for converting to a <see cref="Route"/> domain object.
/// </summary>
public record RouteForCreationDto
{
    /// <summary>
    /// Gets or initializes the unique ID of the route to be created.
    /// </summary>
    public int Id { get; init; }
    
    /// <summary>
    /// Gets or sets the name of the route.
    /// </summary>
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, ErrorMessage = "Name must be between 1 and 100 characters.")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the starting validity date of the route.
    /// </summary>
    [Required(ErrorMessage = "ValidFrom is required.")]
    [DataType(DataType.Date, ErrorMessage = "ValidFrom must be a valid date.")]
    public DateTime ValidFrom { get; set; }

    /// <summary>
    /// Gets or sets the ending validity date of the route.
    /// </summary>
    [Required(ErrorMessage = "ValidTo is required.")]
    [DataType(DataType.Date, ErrorMessage = "ValidTo must be a valid date.")]
    [DateGreaterThan("ValidFrom", ErrorMessage = "ValidTo must be greater than ValidFrom.")]
    public DateTime ValidTo { get; set; }

    /// <summary>
    /// Gets or sets the binary-encoded days of the week on which the route is valid.
    /// Represented as an integer, where each bit corresponds to a day (1 = Monday, 7 = Sunday).
    /// </summary>
    [Required(ErrorMessage = "ValidOn is required.")]
    [BinaryDayOfWeek(ErrorMessage = "ValidOn must be a valid binary encoding for days of the week (1-127).")]
    public int ValidOn { get; set; }

    /// <summary>
    /// Converts the DTO into a <see cref="Route"/> domain object.
    /// </summary>
    /// <returns>A <see cref="Route"/> object representing the DTO data.</returns>
    public Route ToRoute()
    {
        return new Route
        {
            Name = this.Name,
            ValidFrom = this.ValidFrom,
            ValidTo = this.ValidTo,
            ValidOn = this.ValidOn,
        };
    }
}