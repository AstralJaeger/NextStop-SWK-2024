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

    public async Task<int> InsertStopPointAsync(StopPoint stopPoint)
    {
        return await template.ExecuteAsync(
            "insert into stoppoint (name, short_name, latitude, longitude) values (@name, @short_name, @latitude, @longitude)",
            new QueryParameter("@name", stopPoint.Name),
            new QueryParameter("@short_name", stopPoint.ShortName),
            new QueryParameter("@latitude", stopPoint.Location.Latitude),
            new QueryParameter("@longitude", stopPoint.Location.Longitude)
        );
    }

    public async Task<bool> UpdateStopPointAsync(StopPoint stopPoint)
    {
        return await template.ExecuteAsync(
            "update stoppoint set name = @name, short_name = @short_name, latitude = @latitude, longitude = @longitude where id = @id",
            new QueryParameter("@name", stopPoint.Name),
            new QueryParameter("@short_name", stopPoint.ShortName),
            new QueryParameter("@latitude", stopPoint.Location.Latitude),
            new QueryParameter("@longitude", stopPoint.Location.Longitude),
            new QueryParameter("@id", stopPoint.Id)
        ) == 1;
    }

    public async Task<bool> DeleteStopPointAsync(int id)
    {
        return await template.ExecuteAsync(
            "delete from stoppoint where id = @id",
            new QueryParameter("@id", id)
        ) == 1;
    }

    public async Task<StopPoint?> GetStopPointByIdAsync(int id)
    {
        return await template.QuerySingleAsync(
            "select * from stoppoint where id=@spId", 
            MapRowToStopPoint, new QueryParameter("@spId", id));

    }

    public async Task<IEnumerable<StopPoint>> GetAllStopPointsAsync()
    {
        return await template.QueryAsync(
            "select * from stoppoint", MapRowToStopPoint);

    }


    public async Task<IEnumerable<Route>> GetRoutesByStopPointAsync(int stopPointId)
    {
        const string query = @"
        SELECT r.*
        FROM route r
        INNER JOIN routestoppoint rsp ON r.id = rsp.route_id
        WHERE rsp.stop_point_id = @stopPointId";

        return await template.QueryAsync(
            query,
            RouteDao.MapRowToRoute,
            new QueryParameter("@stopPointId", stopPointId)
        );
    }

    public async Task<StopPoint?> GetStopPointByShortNameAsync(string shortName)
    {
        return await template.QuerySingleAsync(
            "select * from stoppoint where short_name=@spName",
            MapRowToStopPoint,
            new QueryParameter("@spName", shortName)
        );
    }

    public async Task<StopPoint?> GetStopPointByNameAsync(string name)
    {
        return await template.QuerySingleAsync(
            "select * from stoppoint where name=@spName",
            MapRowToStopPoint,
            new QueryParameter("@spName", name)
        );
    }
}