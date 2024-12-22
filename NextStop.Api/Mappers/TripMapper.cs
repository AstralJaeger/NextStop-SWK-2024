using NextStop.Api.DTOs;
using NextStop.Domain;
using Riok.Mapperly.Abstractions;

namespace NextStop.Api.Mappers;

/// <summary>
/// Provides mapping functionality between <see cref="Trip"/> domain objects and <see cref="TripDto"/> data transfer objects.
/// </summary>
[Mapper]
public static partial class TripMapper
{
    /// <summary>
    /// Maps a <see cref="Trip"/> domain object to a <see cref="TripDto"/>.
    /// </summary>
    /// <param name="trip">The <see cref="Trip"/> object to map.</param>
    /// <returns>A <see cref="TripDto"/> containing the mapped data.</returns>
    public static partial TripDto ToTripDto(this Trip trip);
    
    /// <summary>
    /// Maps a <see cref="TripDto"/> data transfer object to a <see cref="Trip"/> domain object.
    /// </summary>
    /// <param name="tripDto">The <see cref="TripDto"/> to map.</param>
    /// <returns>A <see cref="Trip"/> object containing the mapped data.</returns>
    public static partial Trip ToTrip(this TripDto tripDto);
}