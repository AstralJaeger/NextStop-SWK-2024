using System.Data;
using NextStop.Common;
using NextStop.Dal.Interface;
using NextStop.Domain;

namespace NextStop.Dal.Ado;

/// <summary>
/// Data Access Object (DAO) implementation for managing Trip-related operations in the database.
/// </summary>
public class TripDao(IConnectionFactory connectionFactory) : ITripDao
{
    /// <summary>
    /// An instance of <see cref="AdoTemplate"/> used to simplify database operations,
    /// such as executing queries, retrieving data, and mapping results to objects.
    /// </summary>
    private readonly AdoTemplate template = new AdoTemplate(connectionFactory);

    //......................................................................
    
    /// <summary>
    /// Maps a database row to a <see cref="Trip"/> object.
    /// </summary>
    /// <param name="row">The database row to map.</param>
    /// <returns>A <see cref="Trip"/> object containing the mapped data.</returns>
    private static Trip MapRowToTrip(IDataRecord row)
        => new Trip(
            id: (int)row["id"],
            routeId: (int)row["route_id"],
            vehicleId: (int)row["vehicle_id"]
        );
    
    //**********************************************************************************
    //**********************************************************************************

    /// <inheritdoc />
    public async Task<int> InsertTripAsync(Trip trip)
    {
        return await template.ExecuteAsync(
            "insert into trip (route_id, vehicle_id) values (@routeid, @vehicleid)",
            new QueryParameter("@routeid", trip.RouteId),
            new QueryParameter("@vehicleid", trip.VehicleId));
    }

    //**********************************************************************************
    //**********************************************************************************

    /// <inheritdoc />
    public async Task<IEnumerable<Trip>> GetAllTripsAsync()
    {
        return await template.QueryAsync(
            "select * from trip", 
            MapRowToTrip);

    }
    
    //......................................................................

    /// <inheritdoc />
    public async Task<Trip?> GetTripByIdAsync(int tripId)
    {
        return await template.QuerySingleAsync("select * from trip where id=@id", 
            MapRowToTrip, 
            new QueryParameter("@id", tripId));

    }

    //......................................................................

    /// <inheritdoc />
    public async Task<IEnumerable<Trip>> GetTripsByRouteIdAsync(int routeId)
    {
        return await template.QueryAsync(
            "select * from trip where route_id = @routeid", 
            MapRowToTrip, 
            new QueryParameter("@routeid", routeId));

    }
    
    //......................................................................

    /// <inheritdoc />
    public async Task<IEnumerable<Trip>> GetTripsByVehicleIdAsync(int vehicleId)
    {
        return await template.QueryAsync(
            "select * from trip where vehicle_id=@vehicleId", 
            MapRowToTrip, 
            new QueryParameter("@vehicleId", vehicleId));

    }
    
}