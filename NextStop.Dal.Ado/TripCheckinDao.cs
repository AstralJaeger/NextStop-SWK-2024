using NextStop.Common;
using System.Data;
using NextStop.Api.DTOs;
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
    
    
    //......................................................................

  
    private TripDelayStatistics MapRowToTripDelayStatistics(IDataRecord row)
    {
        return new TripDelayStatistics
        {
            TripId = row.GetInt32(row.GetOrdinal("TripId")),
            AverageDelay = row.GetDouble(row.GetOrdinal("AverageDelay")),
            TotalStopPoints = row.GetInt32(row.GetOrdinal("TotalStopPoints")),
            OnTimePercentage = row.GetDouble(row.GetOrdinal("OnTimePercentage")),
            SlightlyLatePercentage = row.GetDouble(row.GetOrdinal("SlightlyLatePercentage")),
            LatePercentage = row.GetDouble(row.GetOrdinal("LatePercentage")),
            VeryLatePercentage = row.GetDouble(row.GetOrdinal("VeryLatePercentage"))
        };
    }
    
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
    public async Task<TripDelayStatistics?> GetTripDelayStatisticsAsync(int tripId)
    {
        return await template.QuerySingleAsync(
            @"SELECT
            t.trip_id AS TripId,
            COALESCE(AVG(t.delay), 0) AS AverageDelay,
            COUNT(DISTINCT t.stop_point_id) AS TotalStopPoints,
            COALESCE(SUM(CASE WHEN t.delay < 2 THEN 1 ELSE 0 END) * 100.0 / COUNT(DISTINCT t.stop_point_id), 0) AS OnTimePercentage,
            COALESCE(SUM(CASE WHEN t.delay BETWEEN 2 AND 5 THEN 1 ELSE 0 END) * 100.0 / COUNT(DISTINCT t.stop_point_id), 0) AS SlightlyLatePercentage,
            COALESCE(SUM(CASE WHEN t.delay BETWEEN 5 AND 10 THEN 1 ELSE 0 END) * 100.0 / COUNT(DISTINCT t.stop_point_id), 0) AS LatePercentage,
            COALESCE(SUM(CASE WHEN t.delay > 10 THEN 1 ELSE 0 END) * 100.0 / COUNT(DISTINCT t.stop_point_id), 0) AS VeryLatePercentage
        FROM
            tripcheckin t
        WHERE
            t.trip_id = @tripId
        GROUP BY
            t.trip_id;",
            MapRowToTripDelayStatistics,
            new QueryParameter("@tripId", tripId)
        );
    }
    

    //......................................................................

    /// <inheritdoc />
    public async Task<DateTime> GetArrivalTimeByRouteStopPointAsync(int routeStopPointId)
    {
        return await template.QuerySingleAsync(
            "SELECT arrival_time FROM routestoppoint WHERE id = @routeStopPointId",
            row => (DateTime)row["arrival_time"],
            new QueryParameter("@routeStopPointId", routeStopPointId)
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