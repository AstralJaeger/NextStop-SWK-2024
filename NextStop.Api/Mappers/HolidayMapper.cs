using NextStop.Api.DTOs;
using NextStop.Domain;
using Riok.Mapperly.Abstractions;

namespace NextStop.Api.Mappers;

/// <summary>
/// Provides mapping functionality between <see cref="Holiday"/> domain objects and <see cref="HolidayDto"/> data transfer objects.
/// </summary>
[Mapper]
public static partial class HolidayMapper
{

    /// <summary>
    /// Maps a <see cref="Holiday"/> domain object to a <see cref="HolidayDto"/>.
    /// </summary>
    /// <param name="holiday">The <see cref="Holiday"/> object to map.</param>
    /// <returns>A <see cref="HolidayDto"/> containing the mapped data.</returns>
    public  static partial HolidayDto ToHolidayDto(this Holiday holiday);
    
    /// <summary>
    /// Maps a <see cref="HolidayDto"/> data transfer object to a <see cref="Holiday"/> domain object.
    /// </summary>
    /// <param name="holidayDto">The <see cref="HolidayDto"/> to map.</param>
    /// <returns>A <see cref="Holiday"/> object containing the mapped data.</returns>
    public static partial Holiday ToHoliday(this HolidayDto holidayDto);

    //public static partial void UpdateHoliday(this Holiday holiday, HolidayDto holidayDto);
    
    
}
