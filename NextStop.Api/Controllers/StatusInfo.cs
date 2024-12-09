using Microsoft.AspNetCore.Mvc;

namespace NextStop.Api.Controllers;

public class StatusInfo
{
    public static ProblemDetails InvalidHolidayId(int holidayId) => new ProblemDetails
    {
        Title = "Invalid holiday ID", // Titel des Fehlers.
        Detail = $"Holiday with ID '{holidayId}' does not exist" // Beschreibung des Fehlers.
    };

    public static ProblemDetails InvalidYearForHolidays(int year) => new ProblemDetails
        {
            Title = "Invalid year for holidays", // Titel des Fehlers.
            Detail = $"Holidays for the year '{year}' do not exist" // Beschreibung des Fehlers.
        };

    public static ProblemDetails HolidayAlreadyExists(int holidayId) => new ProblemDetails
    {
        Title = "Conflicting holiday IDs", // Kurze Beschreibung des Problems.
        Detail = $"Holiday id ID '{holidayId}' already exists" // Detaillierte Erklärung.
    };
    
}