using System.Data;
using NextStop.Common;
using NextStop.Dal.Interface;
using NextStop.Domain;

namespace NextStop.Dal.Ado;

public class HolidayDAO (IConnectionFactory connectionFactory) : IHolidayDAO
{
    
    private readonly AdoTemplate template = new AdoTemplate(connectionFactory);

    
    private Holiday MapRowToHoliday(IDataRecord row)
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

    public Task<bool> UpdateHolidayAsync(Holiday holiday)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteHolidayAsync(int holidayId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsHolidayAsync(DateTime date)
    {
        throw new NotImplementedException();
    }

    public async Task<Holiday?> GetHolidayByIdAsync(int holidayId)
    {
        return await template.QuerySingleAsync("select * from holiday where id=@id", MapRowToHoliday, new QueryParameter("@id", holidayId));
    }

    public async Task<IEnumerable<Holiday>> GetAllHolidaysAsync()
    {
        return await template.QueryAsync("select * from holiday", MapRowToHoliday);

    }

    public Task<IEnumerable<Holiday>> GetHolidaysByYearAsync(int year)
    {
        throw new NotImplementedException();
    }
}