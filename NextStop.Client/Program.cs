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

        var updatedHoliday = await holidayDao.GetHolidayByIdAsync(insertResult);
        if (updatedHoliday != null)
        {
            Console.WriteLine("Updated holiday: " + updatedHoliday);
        }

        
        bool deleteResult = await holidayDao.DeleteHolidayAsync(insertResult);
        Console.WriteLine(deleteResult ? "Holiday deleted successfully!" : "Failed to delete holiday.");
     
        var deletedHoliday = await holidayDao.GetHolidayByIdAsync(insertResult);
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
    
    
    
    static async Task Main(string[] args)
    {
        IConfiguration configuration = ConfigurationUtil.GetConfiguration();
        IConnectionFactory connectionFactory =
            DefaultConnectionFactory.FromConfiguration(configuration,
                "PersonDbConnection", "ProviderName");
        await testHolidayDao(connectionFactory);

    }
}


