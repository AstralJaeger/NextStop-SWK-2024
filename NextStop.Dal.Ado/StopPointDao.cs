using System.Data;
using NextStop.Common;
using NextStop.Dal.Interface;
using NextStop.Domain;

namespace NextStop.Dal.Ado;

public class StopPointDao (IConnectionFactory connectionFactory): IStopPointDao
{
    
    private readonly AdoTemplate template = new AdoTemplate(connectionFactory);

    
    private static StopPoint MapRowToStopPoint(IDataRecord row)
        => new StopPoint(
            id: (int)row["id"],
            name: (string)row["name"],
            shortName: (string)row["short_name"],
            location: new Coordinates(
                latitude:(double)row["latitude"],
                longitude:(double)row["longitude"]
            )
        );
    
    
    public async Task<int> InsertAsync(StopPoint stopPoint)
    {
        return await template.ExecuteAsync(
            "insert into stoppoint (name, short_name, latitude, longitude) values (@name, @shortName, @latitude, @longitude)",
            new QueryParameter("@name", stopPoint.Name),
            new QueryParameter("@shortName", stopPoint.ShortName),
            new QueryParameter("@latitude", stopPoint.Location.Latitude),
            new QueryParameter("@longitude", stopPoint.Location.Longitude));
    }

    public async Task<bool> UpdateAsync(StopPoint stopPoint)
    {
        return await template.ExecuteAsync(
            "update stoppoint set name = @name, short_name = @shortName, latitude = @latitude, longitude = @longitude where id = @id",
            new QueryParameter("@name", stopPoint.Name),
            new QueryParameter("@shortName", stopPoint.ShortName),
            new QueryParameter("@latitude", stopPoint.Location.Latitude),
            new QueryParameter("@longitude", stopPoint.Location.Longitude),
            new QueryParameter("@id", stopPoint.Id) ) == 1;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await template.ExecuteAsync(
            "delete from stoppoint where id=@spId",
            new QueryParameter("@spId", id)) == 1;
    }

    public async Task<StopPoint?> GetByIdAsync(int id)
    {
        return await template.QuerySingleAsync("select * from stoppoint where id=@spId", MapRowToStopPoint, new QueryParameter("@spId", id));

    }

    public async Task<IEnumerable<StopPoint>> GetAllStopPointsAsync()
    {
        return await template.QueryAsync("select * from stoppoint", MapRowToStopPoint);

    }
}