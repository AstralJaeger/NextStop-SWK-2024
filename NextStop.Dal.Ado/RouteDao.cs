using System.Data;
using NextStop.Common;
using NextStop.Dal.Interface;
using NextStop.Domain;

namespace NextStop.Dal.Ado;

/// <summary>
/// Data Access Object for managing Route-related database operations.
/// </summary>
public class RouteDao(IConnectionFactory connectionFactory) : IRouteDao
{

    /// <summary>
    /// An instance of <see cref="AdoTemplate"/> used to simplify database operations,
    /// such as executing queries, retrieving data, and mapping results to objects.
    /// </summary>
    private readonly AdoTemplate template = new AdoTemplate(connectionFactory);
    
    //......................................................................

    /// <summary>
    /// Maps a database row to a <see cref="Route"/> object.
    /// </summary>
    /// <param name="row">The database row to map.</param>
    /// <returns>A <see cref="Route"/> object containing the mapped data.</returns>
    public static Route MapRowToRoute(IDataRecord row)
        => new Route
        {
            Id = (int)row["id"],
            Name = (string)row["name"],
            ValidFrom = (DateTime)row["valid_from"],
            ValidTo = (DateTime)row["valid_to"],
            ValidOn = (int)row["valid_on"],
            };
    
    //**********************************************************************************
    //**********************************************************************************

    /// <inheritdoc />

    public async Task<int> InsertRouteAsync(Route route)
    {
        return await template.QueryScalarAsync<int>(
            @"INSERT INTO route (name, valid_from, valid_to, valid_on) 
          VALUES (@name, @valid_from, @valid_to, @valid_on) 
          RETURNING id;",
            new QueryParameter("@name", route.Name),
            new QueryParameter("@valid_from", route.ValidFrom),
            new QueryParameter("@valid_to", route.ValidTo),
            new QueryParameter("@valid_on", route.ValidOn)
        );
    }

    //**********************************************************************************
    //**********************************************************************************

    /// <inheritdoc />
    public async Task<IEnumerable<Route>> GetAllRoutesAsync()
    {
        return await template.QueryAsync(
            "SELECT * FROM route",
            MapRowToRoute);
    }

    //......................................................................

    /// <inheritdoc />
    public async Task<Route?> GetRouteByIdAsync(int id)
    {
        return await template.QuerySingleAsync(
            "SELECT * FROM route WHERE id = @id",
            MapRowToRoute,
            new QueryParameter("@id", id));
    }

    //......................................................................

    /// <inheritdoc />
    public async Task<Route?> GetRouteByNameAsync(string name)
    {
        return await template.QuerySingleAsync(
            "SELECT * FROM route WHERE name = @name",
            MapRowToRoute,
            new QueryParameter("@name", name));
    }

    //......................................................................

    /// <inheritdoc />
    public async Task<IEnumerable<Route>> GetRouteByValidFromAsync(DateTime validFrom)
    {
        return await template.QueryAsync(
            "SELECT * FROM route WHERE valid_from >= @validFrom", 
            MapRowToRoute, new QueryParameter("@validFrom", validFrom));
    }

    //......................................................................

    /// <inheritdoc />
    public async Task<IEnumerable<Route>> GetRouteByValidToAsync(DateTime validTo)
    {
        return await template.QueryAsync(
            "SELECT * FROM route WHERE valid_to <= @validTo",
            MapRowToRoute, 
            new QueryParameter("@validTo", validTo));
    }
    
}