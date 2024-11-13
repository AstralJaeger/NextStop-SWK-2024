using NextStop.Dal.Interface;
using NextStop.Domain;
using System;
using System.Collections.Generic;

namespace NextStop.Dal.Simple;

/// <summary>
/// A simple implementation of IHolidayDAO that stores data in a static in-memory list.
/// </summary>
public class SimpleHolidayDAO
{
    private static IList<Holiday> holidayList = new List<Holiday>
    {
        new Holiday { Id = 1, Name = "Maria Empfängnis", Start = new DateTime(2024, 12, 8), End = new DateTime(2024, 12, 8), Type = HolidayType.NationalHoliday},
        new Holiday { Id = 2, Name = "Weihnachten", Start = new DateTime(2024, 12, 25), End = new DateTime(2024, 12, 25), Type = HolidayType.NationalHoliday},
        new Holiday { Id = 3, Name = "Stefanitag", Start = new DateTime(2024, 12, 26), End = new DateTime(2024, 12, 26), Type = HolidayType.NationalHoliday},
        new Holiday { Id = 4, Name = "Neujahr", Start = new DateTime(2025, 1, 1), End = new DateTime(2025, 1, 1), Type = HolidayType.NationalHoliday},
        new Holiday { Id = 5, Name = "Heilige Drei Könige", Start = new DateTime(2025, 1, 6), End = new DateTime(2025, 1, 6), Type = HolidayType.NationalHoliday},
        new Holiday { Id = 6, Name = "Ostersonntag", Start = new DateTime(2025, 4, 20), End = new DateTime(2025, 4, 20), Type = HolidayType.NationalHoliday},
        new Holiday { Id = 7, Name = "Ostermontag", Start = new DateTime(2025, 4, 21), End = new DateTime(2025, 4, 21), Type = HolidayType.NationalHoliday},
        new Holiday { Id = 8, Name = "Staatsfeiertag", Start = new DateTime(2025, 5, 1), End = new DateTime(2025, 5, 1), Type = HolidayType.NationalHoliday},
        new Holiday { Id = 9, Name = "Christi Himmelfahrt", Start = new DateTime(2025, 5, 29), End = new DateTime(2025, 5, 29), Type = HolidayType.NationalHoliday},
        new Holiday { Id = 10, Name = "Pfingstsonntag", Start = new DateTime(2025, 6, 8), End = new DateTime(2025, 6, 8), Type = HolidayType.NationalHoliday},

    };

    public IEnumerable<Holiday> GetAll()
    {
        return holidayList;
    }

    public Holiday GetById(int id)
    {
        return null; //TODO 
    }

    public void Insert(Holiday holiday)
    {
        holidayList.Add(holiday);
    }

    public void Update(Holiday holiday)
    {
        //todo 
    }

    public void Delete(Holiday holiday)
    {
        holidayList.Remove(holiday);
    }
}