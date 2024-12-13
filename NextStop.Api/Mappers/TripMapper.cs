using NextStop.Api.DTOs;
using NextStop.Domain;
using Riok.Mapperly.Abstractions;

namespace NextStop.Api.Mappers;

[Mapper]
public static partial class TripMapper
{
    public static partial TripDto ToTripDto(this Trip trip);
    
    public static partial Trip ToTrip(this TripDto tripDto);
}