using NextStop.Api.DTOs;
using NextStop.Domain;
using Riok.Mapperly.Abstractions;
using Route = NextStop.Domain.Route;

namespace NextStop.Api.Mappers;

/// <summary>
/// Provides mapping functionality between <see cref="Route"/> domain objects and <see cref="RouteDto"/> data transfer objects.
/// </summary>
[Mapper]
public static partial class RouteMapper
{
    /// <summary>
    /// Maps a <see cref="Route"/> domain object to a <see cref="RouteDto"/>.
    /// </summary>
    /// <param name="route">The <see cref="Route"/> object to map.</param>
    /// <returns>A <see cref="RouteDto"/> containing the mapped data.</returns>
    public static partial RouteDto ToRouteDto(this Route route);
    
    /// <summary>
    /// Maps a <see cref="RouteDto"/> data transfer object to a <see cref="Route"/> domain object.
    /// </summary>
    /// <param name="routeDto">The <see cref="RouteDto"/> to map.</param>
    /// <returns>A <see cref="Route"/> object containing the mapped data.</returns>
    public static partial Route ToRoute(this RouteDto routeDto);
    
}