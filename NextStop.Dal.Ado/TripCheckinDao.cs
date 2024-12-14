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
            checkin: (DateTime)row["checkin_time"]
        ); 

    //**********************************************************************************
    //**********************************************************************************

    /// <inheritdoc />
    public async Task<int> InsertTripCheckinAsync(TripCheckin tripCheckIn)
    {
        return await template.ExecuteAsync(
            "insert into tripcheckin (id, trip_id, stop_point_id, checkin_time) values (@tripCheckinId, @tripId, @stopPointId, @checkin)",
            new QueryParameter("@tripCheckinId", tripCheckIn.Id),
            new QueryParameter("@tripId", tripCheckIn.TripId),
            new QueryParameter("@stopPointId", tripCheckIn.StopPointId),
            new QueryParameter("@checkin", tripCheckIn.CheckIn));
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
    

}