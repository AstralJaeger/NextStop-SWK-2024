﻿namespace NextStop.Domain;

/// <summary>
/// Represents a public holiday with a name, start and end dates, and type (e.g., bank holiday or school vacation).
/// </summary>
public class Holiday
{
    /// <summary>
    /// Gets or sets the unique identifier for the holiday.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the holiday.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the start date and time of the holiday.
    /// </summary>
    public DateTime Start { get; set; }

    /// <summary>
    /// Gets or sets the end date and time of the holiday.
    /// </summary>
    public DateTime End { get; set; }

    /// <summary>
    /// Gets or sets the type of the holiday (e.g., "BankHoliday", "SchoolVacation").
    /// </summary>
    public HolidayType Type { get; set; }
}