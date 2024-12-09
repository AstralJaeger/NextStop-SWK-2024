using NextStop.Api.DTOs;
using NextStop.Domain;
using Riok.Mapperly.Abstractions;

namespace NextStop.Api.Mappers;

[Mapper]
public static partial class StopPointMapper
{
    public static partial StopPointDto ToStopPointDto(this StopPoint stopPoint);
    public static partial IEnumerable<StopPointDto> ToStopPointDtos(this IEnumerable<StopPoint> stopPoints);
    
    public static partial StopPoint ToStopPoint(this StopPointDto stopPoint);
    
    public static partial void UpdateStopPoint(this StopPoint stopPoint, StopPointDto stopPointDto);
    
    
    // public static StopPointDto ToStopPointDto(this StopPoint stopPoint)
    // {
    //     return new StopPointDto
    //     {
    //         Id = stopPoint.Id,
    //         Name = stopPoint.Name,
    //         ShortName = stopPoint.ShortName,
    //         Location = new CoordinatesDto
    //         {
    //             Latitude = stopPoint.Location.Latitude,
    //             Longitude = stopPoint.Location.Longitude
    //         }
    //     };
    // }
    //
    // public static StopPoint ToStopPoint(this StopPointForCreationDto stopPointDto)
    // {
    //     return new StopPoint
    //     {
    //         Name = stopPointDto.Name,
    //         ShortName = stopPointDto.ShortName,
    //         Location = new Coordinates
    //         {
    //             Latitude = stopPointDto.Location.Latitude,
    //             Longitude = stopPointDto.Location.Longitude
    //         }
    //     };
    // }
}