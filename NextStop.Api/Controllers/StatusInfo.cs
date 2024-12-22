using Microsoft.AspNetCore.Mvc;
using NextStop.Domain;

namespace NextStop.Api.Controllers;

public static class StatusInfo
{
    public static ProblemDetails InvalidHolidayId(int holidayId) => new ProblemDetails
    {
        Title = "Invalid holiday ID",
        Detail = $"Holiday with ID '{holidayId}' does not exist"
    };

    public static ProblemDetails InvalidYearForHolidays(int year) => new ProblemDetails
        {
            Title = "Invalid year for holidays",
            Detail = $"Holidays for the year '{year}' do not exist"
        };

    public static ProblemDetails HolidayAlreadyExists(int holidayId) => new ProblemDetails
    {
        Title = "Conflicting holiday IDs",
        Detail = $"Holiday id ID '{holidayId}' already exists"
    };

    public static object? InvalidStopPointIds() => new ProblemDetails
    {
        Title = "Invalid stoppoint ID",
        Detail = $"A Stoppoint with an ID does not exist"
    };

    public static object? InvalidStopPointId(int stopPointId) => new ProblemDetails
    {
        Title = "Invalid stoppoint ID",
        Detail = $"Stoppoint with ID '{stopPointId}' does not exist"
    };

    public static object? StopPointAlreadyExists(object stopPointId) => new ProblemDetails
    {
        Title = "Conflicting stoppoint IDs",
        Detail = $"Stoppoint with ID '{stopPointId}' already exists"
    };

    public static object? StopPointIDNotFound(string target, int stopPointId) => new ProblemDetails
    {
        Title = "No stoppoint found",
        Detail = $"Stoppoint for {target} with ID '{stopPointId}' not found"
    };
    
    public static object? StopPointNotFound(double longitude, double latitude, double radius) => new ProblemDetails
    {
        Title = "No stoppoint found",
        Detail = $"Stoppoint at '{longitude},{latitude}' within {radius}m not found"
    };

    public static object? InvalidStopPointShortName(string shortName) => new ProblemDetails
    {
        Title = "Conflicting stoppoint shortname",
        Detail = $"Stoppoint with shortname '{shortName}' already exists" 
    };

    public static object? InvalidStopPointName(string name) => new ProblemDetails
    {
        Title = "Conflicting stoppoint name",
        Detail = $"Stoppoint with name '{name}' already exists"
    };

    public static object? InvalidRouteId(int routeId) => new ProblemDetails
    {
        Title = "Invalid route ID",
        Detail = $"Route with ID '{routeId}' does not exist"
    };

    public static object? InvalidRouteName(string name) => new ProblemDetails
    {
        Title = "Invalid route name",
        Detail = $"Route with name {name}' does not exist"
    };
        
    public static object? InvalidValidToForRoute(string validTo) => new ProblemDetails
    {
        Title = "Invalid valid from date",
        Detail = $"Route which is valid from {validTo}' does not exist"
    };

    public static object? InvalidValidFromForRoute(string validFrom) => new ProblemDetails
    {
        Title = "Invalid valid to date",
        Detail = $"Route which is valid to {validFrom}' does not exist"
    };

    public static object? RouteAlreadyExists(object routeId) => new ProblemDetails
    {
        Title = "Conflicting route IDs",
        Detail = $"Route with ID '{routeId}' already exists"
    };

    public static object? InvalidRouteStopPointId(int stopPointId) => new ProblemDetails
    {
        Title = "Invalid stoppoint ID",
        Detail = $"Stoppoint with ID '{stopPointId}' does not exist"
    };


    public static object? InvalidStopPointArrivalTime(string arrivalTime) => new ProblemDetails
    {
        Title = "Invalid arrival time for Routestoppoint",
        Detail = $"Routestoppoint with arrival time {arrivalTime}' does not exist"
    };

    public static object? InvalidStopPointDepartureTime(string departureTime) => new ProblemDetails
    {
        Title = "Invalid departure time for Routestoppoint",
        Detail = $"Routestoppoint with departure time {departureTime}' does not exist"
    };

    public static object? RouteStopPointAlreadyExists(object routeStopPointId) => new ProblemDetails
    {
        Title = "Conflicting Routestoppoint IDs", 
        Detail = $"Routestoppoint with ID '{routeStopPointId}' already exists" 
    };

    public static object? InvalidTripId(int tripId) => new ProblemDetails
    {
        Title = "Invalid trip ID", 
        Detail = $"Trip with ID '{tripId}' does not exist"
    };

    public static object? InvalidVehicleId(int vehicleId)  => new ProblemDetails
    {
        Title = "Invalid vehicle ID",
        Detail = $"Vehicle with ID '{vehicleId}' does not exist"
    };

    public static object? TripAlreadyExists(object tripId)  => new ProblemDetails
    {
        Title = "Conflicting trip IDs", 
        Detail = $"Trip with ID '{tripId}' already exists"
    };

    public static object? InvalidTripCheckinId(int tripCheckinId) => new ProblemDetails
    {
        Title = "Invalid tripCheckin ID",
        Detail = $"TripCheckin with ID '{tripCheckinId}' does not exist"
    };

    public static object? TripCheckinAlreadyExists(int tripCheckinId) => new ProblemDetails
    {
        Title = "Conflicting tripCheckin IDs", 
        Detail = $"TripCheckin with ID '{tripCheckinId}' already exists"
    };

    public static object? NoConnectionFound(int startPoint, int destinationPoint, DateTime time) => new ProblemDetails
    {
        Title = "No connection found",
        Detail = $"No connection found between {startPoint} and {destinationPoint} at {time}"
    };
}