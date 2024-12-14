using NextStop.Api.DTOs;
using NextStop.Domain;
using Riok.Mapperly.Abstractions;

namespace NextStop.Api.Mappers;

/// <summary>
/// Provides mapping functionality between <see cref="TripCheckin"/> domain objects and <see cref="TripCheckinDto"/> data transfer objects.
/// </summary>
[Mapper]

public static partial class TripCheckinMapper
{
    /// <summary>
    /// Maps a <see cref="TripCheckin"/> domain object to a <see cref="TripCheckinDto"/>.
    /// </summary>
    /// <param name="tripCheckin">The <see cref="TripCheckin"/> object to map.</param>
    /// <returns>A <see cref="TripCheckinDto"/> containing the mapped data.</returns>
    public static partial TripCheckinDto ToTripCheckinDto(this TripCheckin tripCheckin);
    
    /// <summary>
    /// Maps a <see cref="TripCheckinDto"/> data transfer object to a <see cref="TripCheckin"/> domain object.
    /// </summary>
    /// <param name="tripCheckinDto">The <see cref="TripCheckinDto"/> to map.</param>
    /// <returns>A <see cref="TripCheckin"/> object containing the mapped data.</returns>
    public static partial TripCheckin ToTripCheckin(this TripCheckinDto tripCheckinDto);
}