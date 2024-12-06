using NextStop.Api.DTOs;
using NextStop.Domain;

namespace NextStop.Api.Mappers;

[Mapper]
public static partial class HolidayMapper
{
    public static partial HolidayDto ToHolidayDto(this Holiday holiday);
    
    public static partial IEnumerable<HolidayDto> ToHolidaysDto(this IEnumerable<Holiday> holidays);
    
    public static partial Holiday ToHoliday(this HolidayDto holidayDto);
    
    public static partial void UpdateHoliday(this Holiday holiday, HolidayDto holidayDto);
    
    
    
    //  benutzerdefinierte Mapping-Logik:
    // Diese Methoden zeigen, wie das Mapping ohne Mapperly manuell implementiert werden könnte.
    //public static CustomerDto ToCustomerDto(
    //    this Customer customer)
    //    => new()
    //    {
    //        Id = customer.Id,
    //        Name = customer.Name,
    //        ZipCode = customer.ZipCode,
    //        City = customer.City,
    //        Rating = customer.Rating,
    //        TotalRevenue = customer.TotalRevenue
    //    };

    //public static Customer ToCustomer(this CustomerDto customerDto)
    //        => new(customerDto.Id, customerDto.Name, customerDto.ZipCode,
    //            customerDto.City, customerDto.Rating);
}