using NextStop.Api.DTOs;
using NextStop.Domain;
using Riok.Mapperly.Abstractions;
using Route = NextStop.Domain.Route;

namespace NextStop.Api.Mappers;

[Mapper]
public static partial class RouteMapper
{
    public static partial RouteDto ToRouteDto(this Route route);
    
    public static partial IEnumerable<RouteDto> ToRoutesDto(this IEnumerable<Route> routes);
    
    public static partial Route ToRoute(this RouteDto routeDto);
    
}