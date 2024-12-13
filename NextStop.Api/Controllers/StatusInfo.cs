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

    public static object? InvalidStopPointId(int stopPointId) => new ProblemDetails
    {
        Title = "Invalid stoppoint ID", // Titel des Fehlers.
        Detail = $"Stoppoint with ID '{stopPointId}' does not exist"
    };

    public static object? StopPointAlreadyExists(object stopPointId) => new ProblemDetails
    {
        Title = "Conflicting stoppoint IDs", // Kurze Beschreibung des Problems.
        Detail = $"Stoppoint with ID '{stopPointId}' already exists" // Detaillierte Erklärung.
    };

    public static object? InvalidStopPointShortName(string shortName) => new ProblemDetails
    {
        Title = "Conflicting stoppoint shortname", // Kurze Beschreibung des Problems.
        Detail = $"Stoppoint with shortname '{shortName}' already exists" // Detaillierte Erklärung.
    };

    public static object? InvalidStopPointName(string name) => new ProblemDetails
    {
        Title = "Conflicting stoppoint name", // Kurze Beschreibung des Problems.
        Detail = $"Stoppoint with name '{name}' already exists" // Detaillierte Erklärung.
    };

    public static object? InvalidRouteId(int routeId) => new ProblemDetails
    {
        Title = "Invalid route ID", // Titel des Fehlers.
        Detail = $"Route with ID '{routeId}' does not exist"
    };

    public static object? InvalidRouteName(string name) => new ProblemDetails
    {
        Title = "Invalid route name", // Titel des Fehlers.
        Detail = $"Route with name {name}' does not exist"
    };
        
    public static object? InvalidValidToForRoute(string validTo) => new ProblemDetails
    {
        Title = "Invalid valid from date", // Titel des Fehlers.
        Detail = $"Route which is valid from {validTo}' does not exist"
    };

    public static object? InvalidValidFromForRoute(string validFrom) => new ProblemDetails
    {
        Title = "Invalid valid to date", // Titel des Fehlers.
        Detail = $"Route which is valid to {validFrom}' does not exist"
    };

    public static object? RouteAlreadyExists(object routeId) => new ProblemDetails
    {
        Title = "Conflicting route IDs", // Kurze Beschreibung des Problems.
        Detail = $"Route with ID '{routeId}' already exists" // Detaillierte Erklärung.
    };

    public static object? InvalidRouteStopPointId(int stopPointId) => new ProblemDetails
    {
        Title = "Invalid stoppoint ID", // Titel des Fehlers.
        Detail = $"Stoppoint with ID '{stopPointId}' does not exist"
    };


    public static object? InvalidStopPointArrivalTime(string arrivalTime) => new ProblemDetails
    {
        Title = "Invalid arrival time for Routestoppoint", // Titel des Fehlers.
        Detail = $"Routestoppoint with arrival time {arrivalTime}' does not exist"
    };

    public static object? InvalidStopPointDepartureTime(string departureTime) => new ProblemDetails
    {
        Title = "Invalid departure time for Routestoppoint", // Titel des Fehlers.
        Detail = $"Routestoppoint with departure time {departureTime}' does not exist"
    };

    public static object? RouteStopPointAlreadyExists(object routeStopPointId) => new ProblemDetails
    {
        Title = "Conflicting Routestoppoint IDs", // Kurze Beschreibung des Problems.
        Detail = $"Routestoppoint with ID '{routeStopPointId}' already exists" // Detaillierte Erklärung.
    };
}