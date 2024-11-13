using NextStop.Domain;


namespace NextStop.Dal.Simple;

/// <summary>
/// A simple implementation of IStopPointDAO that stores data in a static in-memory list.
/// </summary>
public class SimpleStopPointDao
{
    // Static list to hold stop point data, simulating a database.
    private static IList<StopPoint> stopPointList = new List<StopPoint>
    {
        new StopPoint 
        { 
            Id = 1, 
            Name = "Hauptbahnhof", 
            ShortName = "Hbf", 
            Location = new Coordinates { Latitude = 40.748817, Longitude = -73.985428 },
            StopPointRoutes = new List<Route> 
            {
                new Route { Id = 1, Name = "Green" },
                new Route { Id = 2, Name = "Blue" }
            }
        },
        new StopPoint 
        { 
            Id = 2, 
            Name = "Uferpromenade", 
            ShortName = "UPN", 
            Location = new Coordinates { Latitude = 34.052235, Longitude = -118.243683 },
            StopPointRoutes = new List<Route> 
            {
                new Route { Id = 3, Name = "Red" }
            }
        }
    };

    /// <summary>
    /// Retrieves a stop point by its unique ID.
    /// </summary>
    /// <param name="id">The unique ID of the stop point.</param>
    /// <returns>The stop point object with the specified ID, or null if not found.</returns>
    public StopPoint GetById(int id)
    {
        return null; //todo
    }

    /// <summary>
    /// Retrieves all stop points from the list.
    /// </summary>
    /// <returns>A list of all stop point objects.</returns>
    public List<StopPoint> GetAll()
    {
        return stopPointList.ToList();
    }

    /// <summary>
    /// Inserts a new stop point into the list.
    /// </summary>
    /// <param name="stopPoint">The stop point object to insert.</param>
    public void Insert(StopPoint stopPoint)
    {
        stopPoint.Id = stopPointList.Max(sp => sp.Id) + 1; // Generate a new ID based on the current maximum
        stopPointList.Add(stopPoint);
    }

    /// <summary>
    /// Updates an existing stop point in the list.
    /// </summary>
    /// <param name="stopPoint">The stop point object with updated information.</param>
    public void Update(StopPoint stopPoint)
    {
        var existingStopPoint = GetById(stopPoint.Id);
        if (existingStopPoint != null)
        {
            existingStopPoint.Name = stopPoint.Name;
            existingStopPoint.ShortName = stopPoint.ShortName;
            existingStopPoint.Location = stopPoint.Location;
            existingStopPoint.StopPointRoutes = stopPoint.StopPointRoutes;
        }
    }

    /// <summary>
    /// Deletes a stop point from the list by its unique ID.
    /// </summary>
    /// <param name="id">The unique ID of the stop point to delete.</param>
    public void Delete(int id)
    {
        var stopPoint = GetById(id);
        if (stopPoint != null)
        {
            stopPointList.Remove(stopPoint);
        }
    }
}
