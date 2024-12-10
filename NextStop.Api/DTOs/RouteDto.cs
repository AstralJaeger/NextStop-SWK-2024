using NextStop.Domain;
using Route = NextStop.Domain.Route;

namespace NextStop.Api.DTOs;

public record RouteDto
{
    public int Id { get; init; }
    public string Name { get; set; }
    
    public DateTime ValidFrom { get; set; }
    
    public DateTime ValidTo { get; set; }
        
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