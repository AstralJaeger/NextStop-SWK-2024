﻿using NextStop.Dal.Interface;
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
        new Holiday(id: 1, name: "Maria Empfängnis", start: new DateTime(2024, 12, 8, 0, 0, 0, DateTimeKind.Local), end: new DateTime(2024, 12, 8, 0, 0, 0, DateTimeKind.Local),
            type: HolidayType.NationalHoliday),
        new Holiday(id: 2, name: "Weihnachten", start: new DateTime(2024, 12, 25, 0, 0, 0, DateTimeKind.Local), end: new DateTime(2024, 12, 25, 0, 0, 0, DateTimeKind.Local),
            type: HolidayType.NationalHoliday),
        new Holiday(id: 3, name: "Stefanitag", start: new DateTime(2024, 12, 26, 0, 0, 0, DateTimeKind.Local), end: new DateTime(2024, 12, 26, 0, 0, 0, DateTimeKind.Local),
            type: HolidayType.NationalHoliday),
        new Holiday(id: 4, name: "Neujahr", start: new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Local), end: new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Local),
            type: HolidayType.NationalHoliday),
        new Holiday(id: 5, name: "Heilige Drei Könige", start: new DateTime(2025, 1, 6, 0, 0, 0, DateTimeKind.Local), end: new DateTime(2025, 1, 6, 0, 0, 0, DateTimeKind.Local),
            type: HolidayType.NationalHoliday),
        new Holiday(id: 6, name: "Ostersonntag", start: new DateTime(2025, 4, 20, 0, 0, 0, DateTimeKind.Local), end: new DateTime(2025, 4, 20, 0, 0, 0, DateTimeKind.Local),
            type: HolidayType.NationalHoliday),
        new Holiday(id: 7, name: "Ostermontag", start: new DateTime(2025, 4, 21, 0, 0, 0, DateTimeKind.Local), end: new DateTime(2025, 4, 21, 0, 0, 0, DateTimeKind.Local),
            type: HolidayType.NationalHoliday),
        new Holiday(id: 8, name: "Staatsfeiertag", start: new DateTime(2025, 5, 1, 0, 0, 0, DateTimeKind.Local), end: new DateTime(2025, 5, 1, 0, 0, 0, DateTimeKind.Local),
            type: HolidayType.NationalHoliday),
        new Holiday(id: 9, name: "Christi Himmelfahrt", start: new DateTime(2025, 5, 29, 0, 0, 0, DateTimeKind.Local), end: new DateTime(2025, 5, 29, 0, 0, 0, DateTimeKind.Local),
            type: HolidayType.NationalHoliday),
        new Holiday(id: 10, name: "Pfingstsonntag", start: new DateTime(2025, 6, 8, 0, 0, 0, DateTimeKind.Local), end: new DateTime(2025, 6, 8, 0, 0, 0, DateTimeKind.Local),
            type: HolidayType.NationalHoliday)
    };

    public IEnumerable<Holiday> GetAll()
    {
        return holidayList;
    }

    public Holiday GetById(int id)
    {
        // ToDo
        throw new NotImplementedException();
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