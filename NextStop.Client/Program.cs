using Microsoft.Extensions.Configuration;
using NextStop.Common;
using NextStop.Dal.Ado;
using NextStop.Domain;

class Program
{
    static async Task Main(string[] args)
    {
        IConfiguration configuration = ConfigurationUtil.GetConfiguration();
        IConnectionFactory connectionFactory =
            DefaultConnectionFactory.FromConfiguration(configuration,
                "PersonDbConnection", "ProviderName");
        
        // DAO-Instanz erstellen
        var holidayDao = new HolidayDAO(connectionFactory);

        // Alle Feiertage abrufen und ausgeben
        var holidays = await holidayDao.GetAllHolidaysAsync();

        Console.WriteLine("Feiertage in der Datenbank:");
        foreach (var holiday in holidays)
        {
            Console.WriteLine($"ID: {holiday.Id}, Name: {holiday.Name}, Start: {holiday.Start}, End: {holiday.End}, Type: {holiday.Type}");
        }
    }
}


