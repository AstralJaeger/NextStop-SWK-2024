using System.Data;
using NextStop.Common;
using NextStop.Dal.Interface;
using NextStop.Domain;

namespace NextStop.Dal.Ado;

public class TripDao(IConnectionFactory connectionFactory) : ITripDao
{
    private readonly AdoTemplate template = new AdoTemplate(connectionFactory);

    
    private static Trip MapRowToTrip(IDataRecord row)
        => new Trip(
            id: (int)row["id"],
            routeId: (int)row["route_id"],
            vehicleId: (int)row["vehicle_id"]
        );
    
    
    public async Task<int> InsertTripAsync(Trip trip)
    {
        return await template.ExecuteAsync(
            "insert into trip (route_id, vehicle_id) values (@routeid, @vehicleid)",
            new QueryParameter("@routeid", trip.RouteId),
            new QueryParameter("@vehicleid", trip.VehicleId));
    }

    public async Task<bool> UpdateTripAsync(Trip trip)
    {
        return await template.ExecuteAsync(
            "update trip set route_id = @routeid, vehicle_id = @vehicleid where id = @id",
            new QueryParameter("@routeid", trip.RouteId),
            new QueryParameter("@vehicleid", trip.VehicleId),
            new QueryParameter("@id", trip.Id) ) == 1;
    }

    public async Task<bool> DeleteTripAsync(int tripId)
    {
        return await template.ExecuteAsync(
            "delete from trip where id=@tripId",
            new QueryParameter("@tripId", tripId)) == 1;
    }

    public async Task<Trip?> GetTripByIdAsync(int tripId)
    {
        return await template.QuerySingleAsync("select * from trip where id=@id", MapRowToTrip, new QueryParameter("@id", tripId));

    }

    public async Task<IEnumerable<Trip>> GetTripsByVehicleIdAsync(int vehicleId)
    {
        return await template.QueryAsync("select * from trip where vehicle_id=@vehicleId", MapRowToTrip, new QueryParameter("@vehicleId", vehicleId));

    }

    public async Task<IEnumerable<Trip>> GetAllTripsAsync()
    {
        return await template.QueryAsync("select * from trip", MapRowToTrip);

    }


    public async Task<IEnumerable<Trip>> GetTripsByRouteIdAsync(int routeId)
    {
        return await template.QueryAsync("select * from trip where route_id = @routeid", MapRowToTrip, new QueryParameter("@routeid", routeId));

    }
}