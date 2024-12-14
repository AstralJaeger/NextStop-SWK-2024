using NextStop.Api.DTOs;
using NextStop.Domain;
using Riok.Mapperly.Abstractions;

namespace NextStop.Api.Mappers;

/// <summary>
/// Provides mapping functionality between <see cref="RouteStopPoint"/> domain objects and <see cref="RouteStopPointDto"/> data transfer objects.
/// </summary>
[Mapper]
public static partial class RouteStopPointMapper
{
   /// <summary>
   /// Maps a <see cref="RouteStopPoint"/> domain object to a <see cref="RouteStopPointDto"/>.
   /// </summary>
   /// <param name="routeStopPoint">The <see cref="RouteStopPoint"/> object to map.</param>
   /// <returns>A <see cref="RouteStopPointDto"/> containing the mapped data.</returns>
   public static partial RouteStopPointDto ToRouteStopPointDto(this RouteStopPoint routeStopPoint);
   
   /// <summary>
   /// Maps a <see cref="RouteStopPointDto"/> data transfer object to a <see cref="RouteStopPoint"/> domain object.
   /// </summary>
   /// <param name="routeStopPointDto">The <see cref="RouteStopPointDto"/> to map.</param>
   /// <returns>A <see cref="RouteStopPoint"/> object containing the mapped data.</returns>
   public static partial RouteStopPoint ToRouteStopPoint(this RouteStopPointDto routeStopPointDto);
}