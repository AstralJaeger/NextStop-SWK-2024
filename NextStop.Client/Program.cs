using Microsoft.Extensions.Configuration;
using NextStop.Common;
using NextStop.Dal.Ado;
using NextStop.Domain;

class Program
{
    public static async Task testHolidayDao( IConnectionFactory connectionFactory)
    {
        // DAO-Instanz erstellen
        var holidayDao = new HolidayDAO(connectionFactory);

        // Alle Feiertage abrufen und ausgeben
        var holidays = await holidayDao.GetAllHolidaysAsync();

        Console.WriteLine("Feiertage in der Datenbank:");
        foreach (var holiday in holidays)
        {
            Console.WriteLine(holiday.ToString());
            //Console.WriteLine($"ID: {holiday.Id}, Name: {holiday.Name}, Start: {holiday.Start}, End: {holiday.End}, Type: {holiday.Type}");
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
        }
        else
        {
            Console.WriteLine("Failed to fetch holiday by ID.");
        }
        
        holidayFromDb.Name = "Updated Test Holiday";
        bool updateResult = await holidayDao.UpdateHolidayAsync(holidayFromDb);
        Console.WriteLine(updateResult ? "Holiday updated successfully!" : "Failed to update holiday.");

        var updatedHoliday = await holidayDao.GetHolidayByIdAsync(holidayFromDb.Id);
        if (updatedHoliday != null)
        {
            Console.WriteLine("Updated holiday: " + updatedHoliday);
        }

        
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
        var tripDao = new TripDAO(connectionFactory);

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
            id: 3, // Id wird von der Datenbank generiert //todo max id abrufen 
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
            Console.WriteLine($"Fetched Trip by ID: {newTrip.ToString()}");
        }
        else
        {
            Console.WriteLine("Failed to fetch trip by ID.");
        }

        // Trip aktualisieren
        if (tripById != null)
        {
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
        if (tripById != null)
        {
            bool deleteResult = await tripDao.DeleteTripAsync(tripById.Id);

            if (deleteResult)
            {
                Console.WriteLine("Trip deleted successfully!");
            }
            else
            {
                Console.WriteLine("Failed to delete trip.");
            }
        }
    }
    
    
    
    static async Task Main(string[] args)
    {
        IConfiguration configuration = ConfigurationUtil.GetConfiguration();
        IConnectionFactory connectionFactory =
            DefaultConnectionFactory.FromConfiguration(configuration,
                "PersonDbConnection", "ProviderName");
        
        //await testHolidayDao(connectionFactory);
        
       await TestTripDao(connectionFactory);

    }
}


