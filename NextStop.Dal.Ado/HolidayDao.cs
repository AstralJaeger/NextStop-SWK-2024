using System.Data;
using NextStop.Common;
using NextStop.Dal.Interface;
using NextStop.Domain;

namespace NextStop.Dal.Ado;

public class HolidayDao (IConnectionFactory connectionFactory) : IHolidayDao
{
    
    private readonly AdoTemplate template = new AdoTemplate(connectionFactory);

    
    private static Holiday MapRowToHoliday(IDataRecord row)
        => new Holiday(
            id: (int)row["id"],
            name: (string)row["name"],
            start: (DateTime)row["start_date"],
            end: (DateTime)row["end_date"],
            type: Enum.Parse<HolidayType>((string)row["typ"]) 
        );
    
    
    public async Task<int> InsertHolidayAsync(Holiday holiday)
    {
        return await template.ExecuteAsync(
            "insert into holiday (name, start_date, end_date, typ) values (@name, @start_date, @end_date, @type::holiday_type)",
            new QueryParameter("@name", holiday.Name),
            new QueryParameter("@start_date", holiday.Start),
            new QueryParameter("@end_date", holiday.End),
            new QueryParameter("@type", holiday.Type.ToString()));

    }

    public async Task<bool> UpdateHolidayAsync(Holiday holiday)
    {
        return await template.ExecuteAsync(
            "update holiday set name = @name, start_date = @start_date, end_date = @end_date, typ = @type::holiday_type where id = @id",
            new QueryParameter("@name", holiday.Name),
            new QueryParameter("@start_date", holiday.Start),
            new QueryParameter("@end_date", holiday.End),
            new QueryParameter("@type", holiday.Type.ToString()),
            new QueryParameter("@id", holiday.Id) ) == 1;
    }

    public async Task<bool> DeleteHolidayAsync(int holidayId)
    {
        return await template.ExecuteAsync(
            "delete from holiday where id=@holidayId",
            new QueryParameter("@holidayId", holidayId)) == 1;
    }

    public async Task<bool> IsHolidayAsync(DateTime date)
    {
        var result = await template.QuerySingleAsync<long>(
            "select count(1) from holiday where @date between start_date and end_date",
            row => (long)row[0], 
            new QueryParameter("@date", date.Date)
        );

        return result > 0;
    }

    public async Task<Holiday?> GetHolidayByIdAsync(int holidayId)
    {
        return await template.QuerySingleAsync("select * from holiday where id=@id", MapRowToHoliday, new QueryParameter("@id", holidayId));
    }

    public async Task<IEnumerable<Holiday>> GetAllHolidaysAsync()
    {
        return await template.QueryAsync("select * from holiday", MapRowToHoliday);
    }

    public async Task<IEnumerable<Holiday>> GetHolidaysByYearAsync(int year)
    {
        return await template.QueryAsync("select * from holiday where  EXTRACT(YEAR FROM start_date) =@year", MapRowToHoliday, new QueryParameter("@year", year));
    }
}