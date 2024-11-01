using NextStop.Domain;

namespace NextStop.Dal.Simple;

/// <summary>
/// A simple implementation of IRouteDAO that stores data in a static in-memory list.
/// </summary>
public class SimpleRouteDao
{
    // Static list to hold route data, simulating a database.
    private static IList<Route> routeList = new List<Route>
    {
        new Route 
        { 
            Id = 1, 
            Name = "Green", 
            ValidFrom = new DateTime(2024, 1, 1), 
            ValidTo = new DateTime(2025, 12, 31), 
            ValidOn = 0b0111110, // Example for Mon-Fri
            RouteStopPoints = new List<RouteStopPoint>
            {
                new RouteStopPoint { StopPointId = 1, RouteId = 1, Order = 1 },
                new RouteStopPoint { StopPointId = 2, RouteId = 1, Order = 2 }
            }
        },
        new Route 
        { 
            Id = 2, 
            Name = "Blue", 
            ValidFrom = new DateTime(2024, 3, 1), 
            ValidTo = new DateTime(2025, 9, 30), 
            ValidOn = 0b1010101, // Example for Mon, Wed, Fri, Sun
            RouteStopPoints = new List<RouteStopPoint>
            {
                new RouteStopPoint { StopPointId = 3, RouteId = 2, Order = 1 },
                new RouteStopPoint { StopPointId = 4, RouteId = 2, Order = 2 }
            }
        },
        new Route 
        { 
        Id = 3, 
        Name = "Red", 
        ValidFrom = new DateTime(2024, 8, 8), 
        ValidTo = new DateTime(2025, 11, 5), 
        ValidOn = 0b1010101, // Example for Mon, Wed, Fri, Sun
        RouteStopPoints = new List<RouteStopPoint>
        {
            new RouteStopPoint { StopPointId = 3, RouteId = 2, Order = 1 },
            new RouteStopPoint { StopPointId = 4, RouteId = 2, Order = 2 }
        }
    }
    };

    /// <summary>
    /// Retrieves a route by its unique ID.
    /// </summary>
    /// <param name="id">The unique ID of the route.</param>
    /// <returns>The route object with the specified ID, or null if not found.</returns>
    public Route GetById(int id)
    {
        return null; //todo
    }

    /// <summary>
    /// Retrieves all routes from the list.
    /// </summary>
    /// <returns>A list of all route objects.</returns>
    public List<Route> GetAll()
    {
        return routeList.ToList();
    }

    /// <summary>
    /// Inserts a new route into the list.
    /// </summary>
    /// <param name="route">The route object to insert.</param>
    public void Insert(Route route)
    {
        route.Id = routeList.Max(r => r.Id) + 1; // Generate a new ID based on the current maximum
        routeList.Add(route);
    }

    /// <summary>
    /// Updates an existing route in the list.
    /// </summary>
    /// <param name="route">The route object with updated information.</param>
    public void Update(Route route)
    {
        var existingRoute = GetById(route.Id);
        if (existingRoute != null)
        {
            existingRoute.Name = route.Name;
            existingRoute.ValidFrom = route.ValidFrom;
            existingRoute.ValidTo = route.ValidTo;
            existingRoute.ValidOn = route.ValidOn;
            existingRoute.RouteStopPoints = route.RouteStopPoints;
        }
    }

    /// <summary>
    /// Deletes a route from the list by its unique ID.
    /// </summary>
    /// <param name="id">The unique ID of the route to delete.</param>
    public void Delete(int id)
    {
        var route = GetById(id);
        if (route != null)
        {
            routeList.Remove(route);
        }
    }
}
