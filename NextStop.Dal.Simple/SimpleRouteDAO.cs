// using NextStop.Domain;
//
// namespace NextStop.Dal.Simple;
//
// /// <summary>
// /// A simple implementation of IRouteDAO that stores data in a static in-memory list.
// /// </summary>
// public class SimpleRouteDao
// {
//     static StopPoint  stopPoint1 = new StopPoint
//     {
//         Id = 1,
//         Name = "Hauptbahnhof",
//         ShortName = "Hbf",
//         Location = new Coordinates { Latitude = 40.748817, Longitude = -73.985428 }
//     };
//
//     static StopPoint  stopPoint2 = new StopPoint
//     {
//         Id = 2,
//         Name = "Uferpromenade",
//         ShortName = "UPN",
//         Location = new Coordinates { Latitude = 34.052235, Longitude = -118.243683 }
//     };
//
//     static StopPoint  stopPoint3 = new StopPoint
//     {
//         Id = 3,
//         Name = "Ostpark",
//         ShortName = "OPK",
//         Location = new Coordinates { Latitude = 51.507351, Longitude = -0.127758 }
//     };
//
//     static StopPoint stopPoint4 = new StopPoint
//     {
//         Id = 4,
//         Name = "Industriezeile",
//         ShortName = "IDZ",
//         Location = new Coordinates { Latitude = 48.30694, Longitude = 14.28583 }
//     };
//
//     // Static list to hold route data, simulating a database.
//     private static IList<Route> routeList = new List<Route>
//     {
//         new Route 
//         { 
//             Id = 1, 
//             Name = "Green", 
//             ValidFrom = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Local), 
//             ValidTo = new DateTime(2025, 12, 31, 23, 59, 59, DateTimeKind.Local), 
//             ValidOn = 0b0111110, // Example for Mon-Fri
//             RouteStopPoints = new List<RouteStopPoint>
//             {
//                 new RouteStopPoint { Id = 1, StopPointId = 1, RouteId = 1, Order = 1, StopPoint = stopPoint1, Route = routeList[0] },
//                 new RouteStopPoint { Id = 2, StopPointId = 2, RouteId = 1, Order = 2, StopPoint = stopPoint2, Route = routeList[0] }
//             }
//         },
//         new Route 
//         { 
//             Id = 2, 
//             Name = "Blue", 
//             ValidFrom = new DateTime(2024, 3, 1, 0, 0, 0, DateTimeKind.Local), 
//             ValidTo = new DateTime(2025, 9, 30, 23, 59, 59, DateTimeKind.Local), 
//             ValidOn = 0b1010101, // Example for Mon, Wed, Fri, Sun
//             RouteStopPoints = new List<RouteStopPoint>
//             {
//                 new RouteStopPoint { Id = 3, StopPointId = 3, RouteId = 2, Order = 1, StopPoint = stopPoint3, Route = routeList[1] },
//                 new RouteStopPoint { Id = 4, StopPointId = 4, RouteId = 2, Order = 2, StopPoint = stopPoint4, Route = routeList[1] }
//             }
//         },
//         new Route 
//         { 
//             Id = 3, 
//             Name = "Red", 
//             ValidFrom = new DateTime(2024, 8, 8, 0, 0, 0, DateTimeKind.Local), 
//             ValidTo = new DateTime(2025, 11, 5, 23, 59, 59, DateTimeKind.Local), 
//             ValidOn = 0b1010101, // Example for Mon, Wed, Fri, Sun
//             RouteStopPoints = new List<RouteStopPoint>
//             {
//                 new RouteStopPoint { Id = 5, StopPointId = 3, RouteId = 3, Order = 1, StopPoint = stopPoint3, Route = routeList[2] },
//                 new RouteStopPoint { Id = 6, StopPointId = 4, RouteId = 3, Order = 2, StopPoint = stopPoint4, Route = routeList[2] }
//             }
//         }
//     };
//
//     /// <summary>
//     /// Retrieves a route by its unique ID.
//     /// </summary>
//     /// <param name="id">The unique ID of the route.</param>
//     /// <returns>The route object with the specified ID, or null if not found.</returns>
//     public Route GetById(int id)
//     {
//         // ToDo
//         throw new NotImplementedException();
//     }
//
//     /// <summary>
//     /// Retrieves all routes from the list.
//     /// </summary>
//     /// <returns>A list of all route objects.</returns>
//     public List<Route> GetAll()
//     {
//         return routeList.ToList();
//     }
//
//     /// <summary>
//     /// Inserts a new route into the list.
//     /// </summary>
//     /// <param name="route">The route object to insert.</param>
//     public void Insert(Route route)
//     {
//         route.Id = routeList.Max(r => r.Id) + 1; // Generate a new ID based on the current maximum
//         routeList.Add(route);
//     }
//
//     /// <summary>
//     /// Updates an existing route in the list.
//     /// </summary>
//     /// <param name="route">The route object with updated information.</param>
//     public void Update(Route route)
//     {
//         var existingRoute = GetById(route.Id);
//         if (existingRoute != null)
//         {
//             existingRoute.Name = route.Name;
//             existingRoute.ValidFrom = route.ValidFrom;
//             existingRoute.ValidTo = route.ValidTo;
//             existingRoute.ValidOn = route.ValidOn;
//             existingRoute.RouteStopPoints = route.RouteStopPoints;
//         }
//     }
//
//     /// <summary>
//     /// Deletes a route from the list by its unique ID.
//     /// </summary>
//     /// <param name="id">The unique ID of the route to delete.</param>
//     public void Delete(int id)
//     {
//         var route = GetById(id);
//         if (route != null)
//         {
//             routeList.Remove(route);
//         }
//     }
// }
