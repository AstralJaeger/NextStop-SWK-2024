using Microsoft.Extensions.Configuration;
using NextStop.Common;
using NextStop.Dal.Ado;
using NextStop.Domain;

namespace NextStop.Client;

static class Program
{
    public static async Task TestHolidayDao( IConnectionFactory connectionFactory)
    {
        // DAO-Instanz erstellen
        var holidayDao = new HolidayDao(connectionFactory);

        // Alle Feiertage abrufen und ausgeben
        var holidays = await holidayDao.GetAllHolidaysAsync();

        Console.WriteLine("Feiertage in der Datenbank:");
        foreach (var holiday in holidays)
        {
            Console.WriteLine(holiday.ToString());
            
        }
        
        // Neuen Feiertag erstellen
        var newHoliday = new Holiday
        (
            id: 33, //todo max von holiday id holen!
            name: "Test Holiday",
            start: new DateTime(2025, 1, 1),
            end: new DateTime(2025, 1, 1),
            type: HolidayType.Other
        );
        
        // Feiertag in die Datenbank einfügen
        int insertResult = await holidayDao.InsertHolidayAsync(newHoliday);

        if (insertResult > 0)
        {
            Console.WriteLine("Holiday inserted successfully!");
        }
        else
        {
            Console.WriteLine("Failed to insert holiday.");
        }
        
        DateTime dateToCheck = new DateTime(2025, 1, 1);
        bool isHoliday = await holidayDao.IsHolidayAsync(dateToCheck);

        if (isHoliday)
        {
            Console.WriteLine($"{dateToCheck.ToShortDateString()} is a holiday!");
        }
        else
        {
            Console.WriteLine($"{dateToCheck.ToShortDateString()} is not a holiday.");
        }
        
        var holidayFromDb = await holidayDao.GetHolidayByIdAsync(newHoliday.Id);
        if (holidayFromDb != null)
        {
            Console.WriteLine("Fetched holiday: " + holidayFromDb);

            // Update nur, wenn holidayFromDb nicht null ist
            holidayFromDb.Name = "Updated Test Holiday";
            bool updateResult = await holidayDao.UpdateHolidayAsync(holidayFromDb);
            Console.WriteLine(updateResult ? "Holiday updated successfully!" : "Failed to update holiday.");

            var updatedHoliday = await holidayDao.GetHolidayByIdAsync(holidayFromDb.Id);
            if (updatedHoliday != null)
            {
                Console.WriteLine("Updated holiday: " + updatedHoliday);
            }

            if (updatedHoliday != null)
            {
                bool deleteResult = await holidayDao.DeleteHolidayAsync(updatedHoliday.Id);
                Console.WriteLine(deleteResult ? "Holiday deleted successfully!" : "Failed to delete holiday.");

                var deletedHoliday = await holidayDao.GetHolidayByIdAsync(updatedHoliday.Id);
                if (deletedHoliday == null)
                {
                    Console.WriteLine("Holiday was deleted successfully and is no longer in the database.");
                }
                else
                {
                    Console.WriteLine("Holiday deletion failed. Holiday still exists in the database.");
                }
            }
            else
            {
                Console.WriteLine("Cannot delete holiday because it was not found or updated.");
            }
        }
        else
        {
            Console.WriteLine("Failed to fetch holiday by ID.");
        }

        
        int yearToCheck = 2025;
        var holidaysInYear = await holidayDao.GetHolidaysByYearAsync(yearToCheck);
        Console.WriteLine($"Feiertage im Jahr {yearToCheck}:");
        foreach (var holiday in holidaysInYear)
        {
            Console.WriteLine(holiday.ToString());
        }
    
    }
    
    
    public static async Task TestTripDao(IConnectionFactory connectionFactory)
    {
        // DAO-Instanz erstellen
        var tripDao = new TripDao(connectionFactory);

        // Alle Trips abrufen und ausgeben
        var trips = await tripDao.GetAllTripsAsync();

        Console.WriteLine("Trips in der Datenbank:");
        foreach (var trip in trips)
        {
            Console.WriteLine(trip.ToString());
        }

        // Neuen Trip erstellen
        var newTrip = new Trip
        (
            id: 0, // Die ID wird von der Datenbank generiert
            routeId: 1, // Beispiel-Routen-ID (stellen Sie sicher, dass diese Route in der Datenbank existiert)
            vehicleId: 103 // Beispiel-Fahrzeug-ID
        );

        // Trip in die Datenbank einfügen
        int insertResult = await tripDao.InsertTripAsync(newTrip);
        if (insertResult > 0)
        {
            Console.WriteLine("Trip inserted successfully!");
        }
        else
        {
            Console.WriteLine("Failed to insert trip.");
        }

        // Neuen Trip aus der Datenbank abrufen
        var tripById = await tripDao.GetTripByIdAsync(newTrip.Id);
        if (tripById != null)
        {
            Console.WriteLine($"Fetched Trip by ID: {tripById.ToString()}");

            // Trip aktualisieren
            tripById.VehicleId = 203; // Beispiel-Update für das Fahrzeug
            bool updateResult = await tripDao.UpdateTripAsync(tripById);

            if (updateResult)
            {
                Console.WriteLine("Trip updated successfully!");
            }
            else
            {
                Console.WriteLine("Failed to update trip.");
            }

            // Trips nach Routen-ID abrufen
            int testRouteId = 1; // Beispiel-Routen-ID
            var tripsByRouteId = await tripDao.GetTripsByRouteIdAsync(testRouteId);

            Console.WriteLine($"Trips für Route ID {testRouteId}:");
            foreach (var trip in tripsByRouteId)
            {
                Console.WriteLine(trip.ToString());
            }

            // Trip löschen
            bool deleteResult = await tripDao.DeleteTripAsync(tripById.Id);

            if (deleteResult)
            {
                Console.WriteLine("Trip deleted successfully!");
            }
            else
            {
                Console.WriteLine("Failed to delete trip.");
            }

            // Überprüfen, ob Trip wirklich gelöscht wurde
            var deletedTrip = await tripDao.GetTripByIdAsync(tripById.Id);
            if (deletedTrip == null)
            {
                Console.WriteLine("Trip was deleted successfully and is no longer in the database.");
            }
            else
            {
                Console.WriteLine("Trip deletion failed. Trip still exists in the database.");
            }
        }
        else
        {
            Console.WriteLine("Failed to fetch trip by ID after insertion. Aborting further operations.");
        }
    }
    
    
    
    public static async Task TestStopPointDao(IConnectionFactory connectionFactory)
    {
        // DAO-Instanz erstellen
        var stopPointDao = new StopPointDao(connectionFactory);

        // Alle StopPoints abrufen und ausgeben
        var stopPoints = await stopPointDao.GetAllStopPointsAsync();

        Console.WriteLine("StopPoints in der Datenbank:");
        foreach (var stopPoint in stopPoints)
        {
            Console.WriteLine(stopPoint.ToString());
        }

        // Neuen StopPoint erstellen
        var newStopPoint = new StopPoint
        (
            id: 0, // Die ID wird von der Datenbank generiert
            name: "Marktplatz",
            shortName: "MTP",
            location: new Coordinates
            {
                Latitude = 40.712776,
                Longitude = -74.005374
            }
        );

        // StopPoint in die Datenbank einfügen
        int insertResult = await stopPointDao.InsertStopPointAsync(newStopPoint);
        if (insertResult > 0)
        {
            Console.WriteLine("StopPoint inserted successfully!");

            // StopPoint nach ID abrufen, um den generierten ID-Wert zu verwenden
            var stopPointById = await stopPointDao.GetRoutesByStopPointAsync(newStopPoint.Id);
            if (stopPointById != null)
            {
                Console.WriteLine($"Retrieved StopPoint by ID: {stopPointById.ToString()}");

                // Update des StopPoints
                stopPointById.Name = "Rathaus";
                stopPointById.ShortName = "RTH";
                bool updateResult = await stopPointDao.UpdateStopPointAsync(stopPointById);
                if (updateResult)
                {
                    Console.WriteLine("StopPoint updated successfully!");

                    // StopPoint erneut nach ID abrufen, um die Änderung zu überprüfen
                    var updatedStopPoint = await stopPointDao.GetRoutesByStopPointAsync(stopPointById.Id);
                    if (updatedStopPoint != null)
                    {
                        Console.WriteLine($"Updated StopPoint: {updatedStopPoint.ToString()}");

                        // StopPoint löschen
                        bool deleteResult = await stopPointDao.DeleteStopPointAsync(updatedStopPoint.Id);
                        if (deleteResult)
                        {
                            Console.WriteLine("StopPoint deleted successfully!");

                            // Überprüfen, ob StopPoint wirklich gelöscht wurde
                            var deletedStopPoint = await stopPointDao.GetStopPointByIdAsync(updatedStopPoint.Id);
                            if (deletedStopPoint == null)
                            {
                                Console.WriteLine("StopPoint was deleted successfully and is no longer in the database.");
                            }
                            else
                            {
                                Console.WriteLine("StopPoint deletion failed. StopPoint still exists in the database.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Failed to delete StopPoint.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Failed to retrieve updated StopPoint by ID.");
                    }
                }
                else
                {
                    Console.WriteLine("Failed to update StopPoint.");
                }
            }
            else
            {
                Console.WriteLine("Failed to retrieve StopPoint by ID after insertion.");
            }
        }
        else
        {
            Console.WriteLine("Failed to insert StopPoint.");
        }
    }
    
    static async Task Main(string[] args)
    {
        IConfiguration configuration = ConfigurationUtil.GetConfiguration();
        IConnectionFactory connectionFactory =
            DefaultConnectionFactory.FromConfiguration(configuration,
                "PersonDbConnection", "ProviderName");
        
        //await testHolidayDao(connectionFactory);
        
       //await TestTripDao(connectionFactory);
       
       await TestStopPointDao(connectionFactory);

    }
}