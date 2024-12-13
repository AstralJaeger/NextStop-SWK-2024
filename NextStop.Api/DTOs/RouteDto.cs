using System.ComponentModel.DataAnnotations;
using NextStop.Domain;
using Route = NextStop.Domain.Route;

namespace NextStop.Api.DTOs;

public record RouteDto
{
    public int Id { get; init; }
    
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, ErrorMessage = "Name must be between 1 and 100 characters.")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "ValidFrom is required.")]
    [DataType(DataType.Date, ErrorMessage = "ValidFrom must be a valid date.")]
    public DateTime ValidFrom { get; set; }
    
    [Required(ErrorMessage = "ValidTo is required.")]
    [DataType(DataType.Date, ErrorMessage = "ValidTo must be a valid date.")]
    [DateGreaterThan("ValidFrom", ErrorMessage = "ValidTo must be greater than ValidFrom.")]
    public DateTime ValidTo { get; set; }
        
    [Required(ErrorMessage = "ValidOn is required.")]
    [Range(1, 7, ErrorMessage = "ValidOn must be a valid day of the week (1 for Monday, 7 for Sunday).")]
    public int ValidOn { get; set; }

    // public Route ToRoute()
    // {
    //     return new Route
    //     {
    //         Name = this.Name,
    //         ValidFrom = this.ValidFrom,
    //         ValidTo = this.ValidTo,
    //         ValidOn = this.ValidOn,
    //     };
    // }
    
}

public record RouteForCreationDto
{
    public int Id { get; init; }
    public string Name { get; set; }

    public DateTime ValidFrom { get; set; }

    public DateTime ValidTo { get; set; }

    public int ValidOn { get; set; }

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