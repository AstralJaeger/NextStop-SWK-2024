using NextStop.Api.DTOs;
using NextStop.Domain;
using Riok.Mapperly.Abstractions;

namespace NextStop.Api.Mappers;

/// <summary>
/// Provides mapping functionality between <see cref="StopPoint"/> domain objects and <see cref="StopPointDto"/> data transfer objects.
/// </summary>
[Mapper]
public static partial class StopPointMapper
{
    /// <summary>
    /// Maps a <see cref="StopPoint"/> domain object to a <see cref="StopPointDto"/>.
    /// </summary>
    /// <param name="stopPoint">The <see cref="StopPoint"/> object to map.</param>
    /// <returns>A <see cref="StopPointDto"/> containing the mapped data.</returns>
    public static partial StopPointDto ToStopPointDto(this StopPoint stopPoint);
    
    /// <summary>
    /// Maps a <see cref="StopPointDto"/> data transfer object to a <see cref="StopPoint"/> domain object.
    /// </summary>
    /// <param name="stopPointDto">The <see cref="StopPointDto"/> to map.</param>
    /// <returns>A <see cref="StopPoint"/> object containing the mapped data.</returns>
    public static partial StopPoint ToStopPoint(this StopPointDto stopPoint);
    
    public static partial void UpdateStopPoint(this StopPoint stopPoint, StopPointDto stopPointDto);
    
}