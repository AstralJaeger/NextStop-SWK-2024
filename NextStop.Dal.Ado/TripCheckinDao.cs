using NextStop.Common;
using System.Data;
using NextStop.Dal.Interface;
using NextStop.Domain;

namespace NextStop.Dal.Ado;

/// <summary>
/// Data Access Object (DAO) implementation for managing StopPoint-related operations in the database.
/// </summary>
public class TripCheckinDao(IConnectionFactory connectionFactory) : ITripCheckinDao
{
    
    /// <summary>
    /// An instance of <see cref="AdoTemplate"/> used to simplify database operations,
    /// such as executing queries, retrieving data, and mapping results to objects.
    /// </summary>
    private readonly AdoTemplate template = new AdoTemplate(connectionFactory);

    //......................................................................

    /// <summary>
    /// Maps a database row to a <see cref="StopPoint"/> object.
    /// </summary>
    /// <param name="row">The database row to map.</param>
    /// <returns>A <see cref="StopPoint"/> object containing the mapped data.</returns>
    private static TripCheckin MapRowToTripCheckin(IDataRecord row)
        => new TripCheckin(
            id: (int)row["id"],
            tripId: (int)row["trip_Id"],
            stopPointId: (int)row["stop_point_Id"],
            checkin: (DateTime)row["checkin_time"],
            delay: (int)row["delay"],
            routeStopPointId: (int)row["routestoppoint_id"]
        ); 

    //**********************************************************************************
    //**********************************************************************************

    /// <inheritdoc />
    public async Task<int> InsertTripCheckinAsync(TripCheckin tripCheckIn)
    {
        Console.WriteLine("InsertTripCheckinAsync");
        return await template.ExecuteAsync(
            "insert into tripcheckin (trip_id, stop_point_id, checkin_time, delay, routestoppoint_id) values (@tripId, @stopPointId, @checkin, @delay, @routeStopPointId)",
            new QueryParameter("@tripId", tripCheckIn.TripId),
            new QueryParameter("@stopPointId", tripCheckIn.StopPointId),
            new QueryParameter("@checkin", tripCheckIn.CheckIn),
            new QueryParameter("@delay", tripCheckIn.Delay),
            new QueryParameter("@routeStopPointId", tripCheckIn.RouteStopPointId));
    }

    //**********************************************************************************
    //**********************************************************************************

    /// <inheritdoc />
    public async Task<IEnumerable<TripCheckin>> GetAllTripCheckinsAsync()
    {
        return await template.QueryAsync(
            "select * from tripcheckin", 
            MapRowToTripCheckin);
    }

    //......................................................................

    /// <inheritdoc />
    public async Task<TripCheckin?> GetTripCheckinByIdAsync(int checkInId)
    {
        return await template.QuerySingleAsync(
            "select * from tripcheckin where id = @checkInId", 
            MapRowToTripCheckin, 
            new QueryParameter("@checkInId", checkInId));
    }

    //......................................................................

    /// <inheritdoc />
    public async Task<IEnumerable<TripCheckin>> GetTripCheckinsByTripIdAsync(int tripId)
    {
        return await template.QueryAsync(
            "select * from tripcheckin where trip_Id = @tripId", 
            MapRowToTripCheckin,
            new QueryParameter("@tripId", tripId));
    }
    
    //......................................................................

    /// <inheritdoc />
    public async Task<IEnumerable<TripCheckin>> GetTripCheckinsByStopPointIdAsync(int stopPointId)
    {
        return await template.QueryAsync(
            "select * from tripcheckin where stop_point_id = @stopPointId", 
            MapRowToTripCheckin,
            new QueryParameter("@stopPointId", stopPointId));
    }

    //......................................................................

    /// <inheritdoc />
    public async Task<IEnumerable<TripCheckin>> GetTripCheckinsByCheckin(DateTime checkIn)
    {
        return await template.QueryAsync(
            "select * from tripcheckin where checkin_time = @checkIn", 
            MapRowToTripCheckin,
            new QueryParameter("@checkIn", checkIn));
    }

    //......................................................................

    /// <inheritdoc />
    public async Task<double> GetAverageDelayForTripAsync(int tripId)
    {
        throw new NotImplementedException();
    }
    

    //......................................................................

    /// <inheritdoc />
    public async Task<DateTime> GetArrivalTimeByRouteAndStopPointAsync(int routeId, int stopPointId)
    {
        return await template.QuerySingleAsync(
            "SELECT arrival_time FROM routestoppoint WHERE route_id = @routeId AND stop_point_id = @stopPointId",
            row => (DateTime)row["arrival_time"],
            new QueryParameter("@routeId", routeId),
            new QueryParameter("@stopPointId", stopPointId)
        );
    }
    
    //......................................................................

    /// <inheritdoc />
    public async Task<int> GetRouteIdByTripIdAsync(int tripId)
    {
        return await template.QuerySingleAsync(
            "SELECT route_id FROM trip WHERE id = @tripId",
            row => (int)row["route_id"],
            new QueryParameter("@tripId", tripId)
        );
    }
    

}