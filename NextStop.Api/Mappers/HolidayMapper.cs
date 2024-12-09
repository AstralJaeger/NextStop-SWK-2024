using NextStop.Api.DTOs;
using NextStop.Domain;
using Riok.Mapperly.Abstractions;

namespace NextStop.Api.Mappers;

[Mapper]
public static partial class HolidayMapper
{
    // Generiert eine Methode, um ein Holiday-Domain-Objekt in ein HolidayDTO zu konvertieren.
    public  static partial HolidayDto ToHolidayDto(this Holiday holiday);

    
    // Generiert eine Methode, um eine Liste von Holiday-Objekten in eine Liste von HolidayDTOs zu konvertieren.
    public  static partial IEnumerable<HolidayDto> ToHolidayDtos(this IEnumerable<Holiday> holidays);

    // Generiert eine Methode, um ein HolidayDTO in ein Holiday-Domain-Objekt zu konvertieren.
    public static partial Holiday ToHoliday(this HolidayDto holidayDto);

    public static partial void UpdateHoliday(this Holiday holiday, HolidayDto holidayDto);

    // public static HolidayDto ToHolidayDto(this Holiday holiday)
    // {
    //     return null;
    // }
    //
    // public static Holiday ToHoliday(this HolidayDto holidayDto)
    // {
    //     return null;
    // }
    //
    // public static void UpdateHoliday(this HolidayDto holidayDto, Holiday holiday)
    // {
    //     
    // }
    
}
