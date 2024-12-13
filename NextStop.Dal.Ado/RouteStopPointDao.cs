using System.Data;
using NextStop.Common;
using NextStop.Dal.Interface;
using NextStop.Domain;

namespace NextStop.Dal.Ado;

public class RouteStopPointDao(IConnectionFactory connectionFactory) :IRouteStopPointDao
{
    private readonly AdoTemplate template = new AdoTemplate(connectionFactory);

    private static RouteStopPoint MapRowToRouteStopPoint(IDataRecord row)
        => new RouteStopPoint
        {
            Id = (int)row["id"],
            RouteId = (int)row["route_id"],
            StopPointId = (int)row["stop_point_id"],
            ArrivalTime = (DateTime)row["arrival_time"],
            DepartureTime = (DateTime)row["departure_time"],
            Order = (int)row["order_number"],

        };

    public async Task InsertRouteStopPointAsync(RouteStopPoint routeStopPoint)
    {
        await template.ExecuteAsync(
            "INSERT INTO routestoppoint (route_id, stop_point_id, arrival_time, departure_time, order_number) " +
            "VALUES (@routeId, @stopPointId, @arrivalTime, @departureTime, @order)",
            new QueryParameter("@routeId", routeStopPoint.RouteId),
            new QueryParameter("@stopPointId", routeStopPoint.StopPointId),
            new QueryParameter("@arrivalTime", routeStopPoint.ArrivalTime),
            new QueryParameter("@departureTime", routeStopPoint.DepartureTime),
            new QueryParameter("@order", routeStopPoint.Order)
        );
    }

    public async Task UpdateRouteStopPointAsync(RouteStopPoint routeStopPoint)
    {
        await template.ExecuteAsync(
            "UPDATE routestoppoint " +
            "SET route_id = @routeId, stop_point_id = @stopPointId, arrival_time = @arrivalTime, " +
            "departure_time = @departureTime, order_number = @order " +
            "WHERE id = @id",
            new QueryParameter("@routeId", routeStopPoint.RouteId),
            new QueryParameter("@stopPointId", routeStopPoint.StopPointId),
            new QueryParameter("@arrivalTime", routeStopPoint.ArrivalTime),
            new QueryParameter("@departureTime", routeStopPoint.DepartureTime),
            new QueryParameter("@order", routeStopPoint.Order),
            new QueryParameter("@id", routeStopPoint.Id)
        );
    }

    public async Task DeleteRouteStopPointAsync(int id)
    {
        await template.ExecuteAsync(
            "DELETE FROM routestoppoint WHERE id = @id",
            new QueryParameter("@id", id)
        );
    }

    public async Task<RouteStopPoint?> GetRouteStopPointByIdAsync(int id)
    {
        return await template.QuerySingleAsync(
            "SELECT * FROM routestoppoint WHERE id = @id",
            MapRowToRouteStopPoint,
            new QueryParameter("@id", id)
        );
    }



    public async Task<IEnumerable<RouteStopPoint>> GetAllRouteStopPointAsync()
    {
        return await template.QueryAsync(
            "SELECT * FROM routestoppoint",
            MapRowToRouteStopPoint
        );
    }
    

    public async Task<IEnumerable<RouteStopPoint>> GetStopPointsByRouteIdAsync(int routeId)
    {
        return await template.QueryAsync(
            "SELECT * FROM routestoppoint WHERE route_id = @routeId",
            MapRowToRouteStopPoint,
            new QueryParameter("@routeId", routeId)
        );
    }

    public async Task<RouteStopPoint?> GetNextRouteStopPointAsync(int routeId, int currentStopOrder)
    {
        return await template.QuerySingleAsync(
            "SELECT * FROM routestoppoint WHERE route_id = @routeId AND order_number = @currentStopOrder + 1",
            MapRowToRouteStopPoint,
            new QueryParameter("@routeId", routeId),
            new QueryParameter("@currentStopOrder", currentStopOrder)
        );
    }

    public async Task<IEnumerable<RouteStopPoint>> GetRouteBetweenStopPointsAsync(int startStopPointId, int endStopPointId)
    {
        return await template.QueryAsync(
            @"SELECT rsp.id, rsp.route_id, rsp.stop_point_id, rsp.arrival_time, rsp.departure_time, rsp.order_number
        FROM routestoppoint rsp
        INNER JOIN (
            SELECT rsp1.route_id
            FROM routestoppoint rsp1
            INNER JOIN routestoppoint rsp2 ON rsp1.route_id = rsp2.route_id
            WHERE rsp1.stop_point_id = @startStopPointId AND rsp2.stop_point_id = @endStopPointId
        ) matching_routes ON rsp.route_id = matching_routes.route_id
        WHERE rsp.order_number BETWEEN 
            (SELECT MIN(order_number) FROM routestoppoint WHERE stop_point_id = @startStopPointId AND route_id = matching_routes.route_id)
            AND
            (SELECT MAX(order_number) FROM routestoppoint WHERE stop_point_id = @endStopPointId AND route_id = matching_routes.route_id)
        ORDER BY rsp.order_number;",
            MapRowToRouteStopPoint,
            new QueryParameter("@startStopPointName", startStopPointId),
            new QueryParameter("@endStopPointName", endStopPointId));
    }

    public async Task<IEnumerable<RouteStopPoint>> GetRouteStopPointsByRouteNameAsync(string routeName)
    {
        return await template.QueryAsync(
            @"SELECT rsp.id, rsp.route_id, rsp.stop_point_id, rsp.arrival_time, rsp.departure_time, rsp.order_number
          FROM routestoppoint rsp
          INNER JOIN route r ON rsp.route_id = r.id
          WHERE r.name = @routeName",
            MapRowToRouteStopPoint,
            new QueryParameter("@routeName", routeName));
    }

    public async Task<RouteStopPoint?> GetStopPointByArrivalTimeAsync(DateTime arrivalTime)
    {
        return await template.QuerySingleAsync(
            @"SELECT id, route_id, stop_point_id, arrival_time, departure_time, order_number
          FROM routestoppoint
          WHERE arrival_time = @arrivalTime",
            MapRowToRouteStopPoint,
            new QueryParameter("@arrivalTime", arrivalTime));
    }

    public async Task<RouteStopPoint?> GetRouteStopPointByDepartureTimeAsync(DateTime departureTime)
    {
        return await template.QuerySingleAsync(
            @"SELECT id, route_id, stop_point_id, arrival_time, departure_time, order_number
          FROM routestoppoint
          WHERE departure_time = @departureTime",
            MapRowToRouteStopPoint,
            new QueryParameter("@departureTime", departureTime));
    }
    
    public async Task<IEnumerable<RouteStopPoint>> GetRoutesByStopPointIdAsync(int stopPointId)
    {
        // SQL-Abfrage, um alle Routen zu finden, die den StopPoint anfahren
        return await template.QueryAsync(
            @"SELECT rsp.id, rsp.route_id, rsp.stop_point_id, rsp.arrival_time, rsp.departure_time, rsp.order_number
          FROM routestoppoint rsp
          WHERE rsp.stop_point_id = @stopPointId",
            MapRowToRouteStopPoint,
            new QueryParameter("@stopPointId", stopPointId));
    }

    public async Task<bool> IsSameRouteForRouteStopPoints(int startId, int endId)
    {
        // SQL-Abfrage, um zu prüfen, ob die beiden StopPoints dieselbe Route haben
        var query = @"
        SELECT COUNT(*) 
        FROM routestoppoint rsp1
        INNER JOIN routestoppoint rsp2 ON rsp1.route_id = rsp2.route_id
        WHERE rsp1.stop_point_id = @startId AND rsp2.stop_point_id = @endId";

        // Verwende QuerySingleAsync, um den Wert von COUNT(*) zu erhalten
        var count = await template.QuerySingleAsync(
            query,
            row => (int)(long)row[0], // Der erste Spaltenwert wird in einen int umgewandelt
            new QueryParameter("@startId", startId),
            new QueryParameter("@endId", endId)
        );

        // Wenn der Wert größer als 0 ist, teilen die beiden StopPoints dieselbe Route
        return count > 0;
        
        
        // var result = await template.ExecuteAsync(
        //     @" SELECT COUNT(*) 
        // FROM routestoppoint rsp1
        // INNER JOIN routestoppoint rsp2 ON rsp1.route_id = rsp2.route_id
        // WHERE rsp1.stop_point_id = @startId AND rsp2.stop_point_id = @endId",
        //     new QueryParameter("@startId", startId),
        //     new QueryParameter("@endId", endId));
        //
        // // Wenn das Ergebnis größer als 0 ist, teilen die beiden StopPoints dieselbe Route
        // return result > 0;
    }
}