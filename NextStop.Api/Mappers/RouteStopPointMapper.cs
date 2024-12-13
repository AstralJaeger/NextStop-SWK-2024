using NextStop.Api.DTOs;
using NextStop.Domain;
using Riok.Mapperly.Abstractions;

namespace NextStop.Api.Mappers;

[Mapper]
public static partial class RouteStopPointMapper
{
   public static partial RouteStopPointDto ToRouteStopPointDto(this RouteStopPoint routeStopPoint);
   
   public static partial RouteStopPoint ToRouteStopPoint(this RouteStopPointDto routeStopPointDto);
}