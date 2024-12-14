using System.Data;
using NextStop.Common;
using NextStop.Dal.Interface;
using NextStop.Domain;

namespace NextStop.Dal.Ado;

/// <summary>
/// Data Access Object for managing Route Stop Point-related database operations.
/// </summary>
public class StopPointDao (IConnectionFactory connectionFactory): IStopPointDao
{
    /// <summary>
    /// An instance of <see cref="AdoTemplate"/> used to simplify database operations,
    /// such as executing queries, retrieving data, and mapping results to objects.
    /// </summary>
    private readonly AdoTemplate template = new AdoTemplate(connectionFactory);

    //......................................................................

    /// <summary>
    /// Maps a database row to a <see cref="RouteStopPoint"/> object.
    /// </summary>
    /// <param name="row">The database row to map.</param>
    /// <returns>A <see cref="RouteStopPoint"/> object containing the mapped data.</returns>
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
    
    //**********************************************************************************
    //**********************************************************************************

    /// <inheritdoc />
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

    //**********************************************************************************
    //**********************************************************************************

    /// <inheritdoc />
    public async Task<IEnumerable<StopPoint>> GetAllStopPointsAsync()
    {
        return await template.QueryAsync(
            "select * from stoppoint", 
            MapRowToStopPoint);

    }
    //......................................................................
    
    /// <inheritdoc />
    public async Task<StopPoint?> GetStopPointByIdAsync(int id)
    {
        return await template.QuerySingleAsync(
            "select * from stoppoint where id=@spId", 
            MapRowToStopPoint, 
            new QueryParameter("@spId", id));

    }
    //......................................................................

    /// <inheritdoc />
    public async Task<IEnumerable<Route>> GetRoutesByStopPointAsync(int stopPointId)
    {

        return await template.QueryAsync(
            @"SELECT r.*
                FROM route r
                INNER JOIN routestoppoint rsp ON r.id = rsp.route_id
                WHERE rsp.stop_point_id = @stopPointId",
            RouteDao.MapRowToRoute,
            new QueryParameter("@stopPointId", stopPointId)
        );
    }

    //......................................................................

    /// <inheritdoc />
    public async Task<StopPoint?> GetStopPointByNameAsync(string name)
    {
        return await template.QuerySingleAsync(
            "select * from stoppoint where name=@spName",
            MapRowToStopPoint,
            new QueryParameter("@spName", name)
        );
    }
    
    //......................................................................

    /// <inheritdoc />
    public async Task<StopPoint?> GetStopPointByShortNameAsync(string shortName)
    {
        return await template.QuerySingleAsync(
            "select * from stoppoint where short_name=@spName",
            MapRowToStopPoint,
            new QueryParameter("@spName", shortName)
        );
    }
    
    
    //**********************************************************************************
    //**********************************************************************************

    /// <inheritdoc />
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
    
    //**********************************************************************************
    //**********************************************************************************

    /// <inheritdoc />
    public async Task<bool> DeleteStopPointAsync(int id)
    {
        return await template.ExecuteAsync(
            "delete from stoppoint where id = @id",
            new QueryParameter("@id", id)
        ) == 1;
    }
    
    //......................................................................

   
}