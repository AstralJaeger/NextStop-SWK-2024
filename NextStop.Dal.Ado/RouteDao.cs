using System.Data;
using NextStop.Common;
using NextStop.Dal.Interface;
using NextStop.Domain;

namespace NextStop.Dal.Ado;

public class RouteDao(IConnectionFactory connectionFactory) : IRouteDao
{

    private readonly AdoTemplate template = new AdoTemplate(connectionFactory);
    private static Route MapRowToRoute(IDataRecord row)
        => new Route
        {
            Id = (int)row["id"],
            Name = (string)row["name"],
            ValidFrom = (DateTime)row["valid_from"],
            ValidTo = (DateTime)row["valid_to"],
            ValidOn = (int)row["valid_on"],
            };
    
    public async Task<int> InsertRouteAsync(Route route)
    {
        return await template.ExecuteAsync(
            "INSERT INTO route (name, valid_from, valid_to, valid_on) VALUES (@name, @valid_from, @valid_to, @valid_on)",
            new QueryParameter("@name", route.Name),
            new QueryParameter("@valid_from", route.ValidFrom),
            new QueryParameter("@valid_to", route.ValidTo),
            new QueryParameter("@valid_on", route.ValidOn));
    }

    public async Task UpdateRouteAsync(Route route)
    {
        await template.ExecuteAsync(
            "UPDATE route SET name = @name, valid_from = @valid_from, valid_to = @valid_to, valid_to = @valid_to WHERE id = @id",
            new QueryParameter("@id", route.Id),
            new QueryParameter("@name", route.Name),
            new QueryParameter("@valid_from", route.ValidFrom),
            new QueryParameter("@valid_to", route.ValidTo),
            new QueryParameter("@valid_to", route.ValidOn));
    }

    public async Task DeleteRouteAsync(int id)
    {
        await template.ExecuteAsync(
            "DELETE FROM route WHERE id = @id",
            new QueryParameter("@id", id));
    }

    public async Task<Route?> GetRouteByIdAsync(int id)
    {
        return await template.QuerySingleAsync(
            "SELECT * FROM route WHERE id = @id",
            MapRowToRoute,
            new QueryParameter("@id", id));
    }

    public async Task<IEnumerable<Route>> GetAllRoutesAsync()
    {
        return await template.QueryAsync(
            "SELECT * FROM route",
            MapRowToRoute);
    }

    public async Task<Route?> GetRouteByNameAsync(string name)
    {
        return await template.QuerySingleAsync(
            "SELECT * FROM route WHERE name = @name",
            MapRowToRoute,
            new QueryParameter("@name", name));
    }

    public async Task<IEnumerable<Route>> GetRouteByValidFromAsync(DateTime validFrom)
    {
        return await template.QueryAsync(
            "SELECT * FROM route WHERE valid_from >= @validFrom", 
            MapRowToRoute, new QueryParameter("@validFrom", validFrom));
    }

    public async Task<IEnumerable<Route>> GetRouteByValidToAsync(DateTime validTo)
    {
        return await template.QueryAsync("SELECT * FROM route WHERE valid_to <= @validTo",
            MapRowToRoute, new QueryParameter("@validTo", validTo));
    }


    // public Task<Task<IEnumerable<Route>>> GetRoutesByValidityDayAsync(int validityDay)
    // {
    //     throw new NotImplementedException();
    // }
}