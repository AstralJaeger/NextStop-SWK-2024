using System.Data;
using NextStop.Common;
using NextStop.Dal.Interface;
using NextStop.Domain;

namespace NextStop.Dal.Ado;

/// <summary>
/// Data Access Object (DAO) implementation for managing Holiday-related operations.
/// </summary>
public class HolidayDao (IConnectionFactory connectionFactory) : IHolidayDao
{
    
    /// <summary>
    /// An instance of <see cref="AdoTemplate"/> used to simplify database operations,
    /// such as executing queries, retrieving data, and mapping results to objects.
    /// </summary>
    private readonly AdoTemplate template = new AdoTemplate(connectionFactory);

    //......................................................................

    /// <summary>
    /// Maps a database row to a <see cref="Holiday"/> object.
    /// </summary>
    /// <param name="row">The data row to map.</param>
    /// <returns>A <see cref="Holiday"/> object populated with the row data.</returns>
    private static Holiday MapRowToHoliday(IDataRecord row)
        => new Holiday(
            id: (int)row["id"],
            name: (string)row["name"],
            start: (DateTime)row["start_date"],
            end: (DateTime)row["end_date"],
            type: Enum.Parse<HolidayType>((string)row["typ"]) 
        );
 
    //**********************************************************************************
    //**********************************************************************************

    
    /// <inheritdoc />
    public async Task<int> InsertHolidayAsync(Holiday holiday)
    {
        return await template.ExecuteAsync(
            "insert into holiday (name, start_date, end_date, typ) values (@name, @start_date, @end_date, @type::holiday_type)",
            new QueryParameter("@name", holiday.Name),
            new QueryParameter("@start_date", holiday.StartDate),
            new QueryParameter("@end_date", holiday.EndDate),
            new QueryParameter("@type", holiday.Type.ToString()));

    }

    //**********************************************************************************
    //**********************************************************************************

    /// <inheritdoc />
    public async Task<IEnumerable<Holiday>> GetAllHolidaysAsync()
    {
        return await template.QueryAsync(
            "select * from holiday", 
            MapRowToHoliday);
    }

    //......................................................................
    
    /// <inheritdoc />
    public async Task<bool> IsHolidayAsync(DateTime date)
    {
        var result = await template.QuerySingleAsync(
            "select count(1) from holiday where @date between start_date and end_date",
            row => (long)row[0], 
            new QueryParameter("@date", date.Date)
        );

        return result > 0;
    }

    //......................................................................

    /// <inheritdoc />
    public async Task<Holiday?> GetHolidayByIdAsync(int holidayId)
    {
        return await template.QuerySingleAsync(
            "select * from holiday where id = @id", 
            MapRowToHoliday, 
            new QueryParameter("@id", holidayId));
    }

    //......................................................................

    /// <inheritdoc />
    public async Task<IEnumerable<Holiday>> GetHolidaysByYearAsync(int year)
    {
        return await template.QueryAsync(
            "select * from holiday where  EXTRACT(YEAR FROM start_date) = @year", 
            MapRowToHoliday, 
            new QueryParameter("@year", year));
    }
    
    //**********************************************************************************
    //**********************************************************************************

    /// <inheritdoc />
    public async Task<bool> UpdateHolidayAsync(Holiday holiday)
    {
        return await template.ExecuteAsync(
            "update holiday set name = @name, start_date = @start_date, end_date = @end_date, typ = @type::holiday_type where id = @id",
            new QueryParameter("@name", holiday.Name),
            new QueryParameter("@start_date", holiday.StartDate),
            new QueryParameter("@end_date", holiday.EndDate),
            new QueryParameter("@type", holiday.Type.ToString()),
            new QueryParameter("@id", holiday.Id) ) == 1;
    }

    //**********************************************************************************
    //**********************************************************************************

    /// <inheritdoc />
    public async Task<bool> DeleteHolidayAsync(int holidayId)
    {
        return await template.ExecuteAsync(
            "delete from holiday where id=@holidayId",
            new QueryParameter("@holidayId", holidayId)) == 1;
    }

   
}