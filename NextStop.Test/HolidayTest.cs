using System.Collections;
using Microsoft.Extensions.Configuration;
using NextStop.Common;
using NextStop.Dal.Ado;
using NextStop.Domain;

namespace NextStop.Test;

[TestFixture]
public class HolidayTest
{
    private IConnectionFactory _connectionFactory;

    public required HolidayDao _dao { get; set; }

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        IConfiguration configuration = ConfigurationUtil.GetConfiguration();
        _connectionFactory =
            DefaultConnectionFactory.FromConfiguration(configuration,
                "PersonDbConnection", "ProviderName");

        _dao = new HolidayDao(_connectionFactory);
    }

    public static readonly Holiday[] TestAustrianHolidays2024 =
    {
        new Holiday(id: 1, name: "Maria Empfängnis", start: new DateTime(2024, 12, 8, 0, 0, 0, DateTimeKind.Local),
            end: new DateTime(2024, 12, 8, 0, 0, 0, DateTimeKind.Local),
            type: HolidayType.NationalHoliday),
        new Holiday(id: 2, name: "Weihnachten", start: new DateTime(2024, 12, 25, 0, 0, 0, DateTimeKind.Local),
            end: new DateTime(2024, 12, 25, 0, 0, 0, DateTimeKind.Local),
            type: HolidayType.NationalHoliday),
        new Holiday(id: 3, name: "Stefanitag", start: new DateTime(2024, 12, 26, 0, 0, 0, DateTimeKind.Local),
            end: new DateTime(2024, 12, 26, 0, 0, 0, DateTimeKind.Local),
            type: HolidayType.NationalHoliday),
        new Holiday(id: 4, name: "Neujahr", start: new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Local),
            end: new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Local),
            type: HolidayType.NationalHoliday),
        new Holiday(id: 5, name: "Heilige Drei Könige", start: new DateTime(2025, 1, 6, 0, 0, 0, DateTimeKind.Local),
            end: new DateTime(2025, 1, 6, 0, 0, 0, DateTimeKind.Local),
            type: HolidayType.NationalHoliday),
        new Holiday(id: 6, name: "Ostersonntag", start: new DateTime(2025, 4, 20, 0, 0, 0, DateTimeKind.Local),
            end: new DateTime(2025, 4, 20, 0, 0, 0, DateTimeKind.Local),
            type: HolidayType.NationalHoliday),
        new Holiday(id: 7, name: "Ostermontag", start: new DateTime(2025, 4, 21, 0, 0, 0, DateTimeKind.Local),
            end: new DateTime(2025, 4, 21, 0, 0, 0, DateTimeKind.Local),
            type: HolidayType.NationalHoliday),
        new Holiday(id: 8, name: "Staatsfeiertag", start: new DateTime(2025, 5, 1, 0, 0, 0, DateTimeKind.Local),
            end: new DateTime(2025, 5, 1, 0, 0, 0, DateTimeKind.Local),
            type: HolidayType.NationalHoliday),
        new Holiday(id: 9, name: "Christi Himmelfahrt", start: new DateTime(2025, 5, 29, 0, 0, 0, DateTimeKind.Local),
            end: new DateTime(2025, 5, 29, 0, 0, 0, DateTimeKind.Local),
            type: HolidayType.NationalHoliday),
        new Holiday(id: 10, name: "Pfingstsonntag", start: new DateTime(2025, 6, 8, 0, 0, 0, DateTimeKind.Local),
            end: new DateTime(2025, 6, 8, 0, 0, 0, DateTimeKind.Local),
            type: HolidayType.NationalHoliday)
    };

    public static readonly Holiday[] TestInsertValidHoliday =
    {
        new Holiday(
            0,
            "Ficsmas",
            new DateTime(2024, 12, 1, 0, 0, 0),
            new DateTime(2025, 1, 18, 23, 59, 59),
            HolidayType.ReligiousHoliday
        ),
        new Holiday(0,
            "Erster August",
            new DateTime(2024, 8, 1, 0, 0, 0),
            new DateTime(2024, 8, 1, 23, 59, 59),
            HolidayType.SchoolVacation)
    };

    [Test(Description = "Insert a new Holiday /'Schönwetterfall/'")]
    [TestCaseSource(nameof(TestInsertValidHoliday))]
    public async Task TestInsertValidHoliday_InsertHolidayAsyncTest(Holiday holiday)
    {
        await _dao.InsertHolidayAsync(holiday);
    }

    [Test(Description = "Update an existing holiday")]
    public async Task TestUpdateExistingHoliday_UpdateHolidayAsyncTest()
    {
        var holidayToUpdate = new Holiday(3,
            "Weihnachten ❄️",
            new DateTime(2024, 12, 25, 0, 0, 0, DateTimeKind.Local),
            new DateTime(2024, 12, 25, 23, 59, 59, DateTimeKind.Local),
            HolidayType.NationalHoliday
        );
        Assert.True(await _dao.UpdateHolidayAsync(holidayToUpdate));
    }

    [Test(Description = "Update a non existing holiday")]
    public async Task TestUpdateNonExistingHoliday_UpdateHolidayAsyncTest()
    {
        var holidayToUpdate = new Holiday(666,
            "NotAHoliday",
            new DateTime(2024, 12, 25, 0, 0, 0, DateTimeKind.Local),
            new DateTime(2024, 12, 25, 23, 59, 59, DateTimeKind.Local),
            HolidayType.NationalHoliday
        );
        Assert.False(await _dao.UpdateHolidayAsync(holidayToUpdate));
    }

    [Test(Description = "Delete an existing holiday")]
    public async Task TestDeleteExistingHoliday_DeleteHolidayAsyncTest()
    {
        // We'll get rid of easter
        Assert.True(await _dao.DeleteHolidayAsync(7));
    }

    [Test(Description = "Delete an existing holiday")]
    public async Task TestDeleteNonExistingHoliday_DeleteHolidayAsyncTest()
    {
        // We'll get rid of a non existing holyday
        Assert.False(await _dao.DeleteHolidayAsync(99));
    }

    public static DateTime[] TestExistingHoliday =
    {
        new(2024, 12, 8, 0, 0, 0, DateTimeKind.Local),
        new(2024, 12, 25, 0, 0, 0, DateTimeKind.Local),
        new(2024, 12, 26, 0, 0, 0, DateTimeKind.Local),
        new(2025, 1, 1, 0, 0, 0, DateTimeKind.Local),
        new(2025, 1, 6, 0, 0, 0, DateTimeKind.Local),
        new(2025, 4, 21, 0, 0, 0, DateTimeKind.Local),
        new(2025, 5, 1, 0, 0, 0, DateTimeKind.Local),
        new(2025, 5, 29, 0, 0, 0, DateTimeKind.Local),
        new(2025, 6, 8, 0, 0, 0, DateTimeKind.Local)
    };

    [Test(Description = "Checks if a holiday is a holiday")]
    [TestCaseSource(nameof(TestExistingHoliday))]
    public async Task TestExistingHoliday_IsHolidayAsyncTest(DateTime date)
    {
        Assert.True(await _dao.IsHolidayAsync(date));
    }

    public static DateTime[] TestNonExistingHoliday =
    {
        new(2024, 2, 7, 0, 0, 0, DateTimeKind.Local),
        new(2024, 8, 7, 0, 0, 0, DateTimeKind.Local)
    };

    [Test(Description = "Checks if a normal is a holiday")]
    [TestCaseSource(nameof(TestNonExistingHoliday))]
    public async Task TestNonExistingHoliday_IsHolidayAsyncTest(DateTime date)
    {
        Assert.False(await _dao.IsHolidayAsync(date));
    }

    public static IEnumerable<TestCaseData> TestGetExistingHoliday
    {
        get
        {
            var holidays = new Dictionary<int, Holiday>()
            {
                [1] = new(id: 1, name: "Maria Empfängnis",
                    start: new DateTime(2024, 12, 8, 0, 0, 0, DateTimeKind.Local),
                    end: new DateTime(2024, 12, 8, 0, 0, 0, DateTimeKind.Local),
                    type: HolidayType.NationalHoliday),
                [2] = new(id: 2, name: "Weihnachtsferien",
                    start: new DateTime(2024, 12, 23, 0, 0, 0, DateTimeKind.Local),
                    end: new DateTime(2025, 1, 6, 0, 0, 0, DateTimeKind.Local),
                    type: HolidayType.SchoolVacation),
                // 3 Weihnachten is skipped due to update on it in other test
                [4] = new(id: 4, name: "Stefanitag",
                    start: new DateTime(2024, 12, 26, 0, 0, 0, DateTimeKind.Local),
                    end: new DateTime(2024, 12, 26, 0, 0, 0, DateTimeKind.Local),
                    type: HolidayType.NationalHoliday),
                [5] = new(id: 5, name: "Neujahr", start: new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Local),
                    end: new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Local),
                    type: HolidayType.NationalHoliday),
                [6] = new(id: 6, name: "Heilige Drei Könige",
                    start: new DateTime(2025, 1, 6, 0, 0, 0, DateTimeKind.Local),
                    end: new DateTime(2025, 1, 6, 0, 0, 0, DateTimeKind.Local),
                    type: HolidayType.NationalHoliday),
                // 7 Ostersonntag is skipped due to delete on it in other test
                [8] = new(id: 8, name: "Ostermontag",
                    start: new DateTime(2025, 4, 21, 0, 0, 0, DateTimeKind.Local),
                    end: new DateTime(2025, 4, 21, 0, 0, 0, DateTimeKind.Local),
                    type: HolidayType.NationalHoliday),
                [9] = new(id: 9, name: "Staatsfeiertag",
                    start: new DateTime(2025, 5, 1, 0, 0, 0, DateTimeKind.Local),
                    end: new DateTime(2025, 5, 1, 0, 0, 0, DateTimeKind.Local),
                    type: HolidayType.NationalHoliday),
                [10] = new(id: 10, name: "Christi Himmelfahrt",
                    start: new DateTime(2025, 5, 29, 0, 0, 0, DateTimeKind.Local),
                    end: new DateTime(2025, 5, 29, 0, 0, 0, DateTimeKind.Local),
                    type: HolidayType.NationalHoliday),
                [11] = new(id: 11, name: "Pfingstsonntag",
                    start: new DateTime(2025, 6, 8, 0, 0, 0, DateTimeKind.Local),
                    end: new DateTime(2025, 6, 8, 0, 0, 0, DateTimeKind.Local),
                    type: HolidayType.NationalHoliday),
                [12] = new(id: 12, name: "Sommerferien", 
                    start: new DateTime(2025, 7, 5, 0, 0, 0, DateTimeKind.Local), 
                    end:new DateTime(2025, 8, 7, 0, 0, 0, DateTimeKind.Local), 
                    type: HolidayType.SchoolVacation)
            };
            
            foreach (var entry in holidays)
            {
                yield return new TestCaseData(entry.Key, entry.Value);
            }
        }
    }

    [Test(Description = "Gets a holiday by ID")]
    [TestCaseSource(nameof(TestGetExistingHoliday))]
    public async Task TestExistingHoliday_GetHolidayByIdAsyncTest(int id, Holiday holiday)
    {
        var result = await _dao.GetHolidayByIdAsync(id);
        Assert.Multiple(() =>
        {
            Assert.NotNull(result);
            Assert.That(holiday.Id, Is.EqualTo(result.Id));
            Assert.That(holiday.Name, Is.EqualTo(result.Name));
            Assert.That(holiday.Start, Is.EqualTo(result.Start));
            Assert.That(holiday.End, Is.EqualTo(result.End));
            Assert.That(holiday.Type, Is.EqualTo(result.Type));
        });
    }

    [Test(Description="Gets a non existing holiday by ID")]
    [TestCase(51)]
    [TestCase(43)]
    [TestCase(1411)]
    public async Task GetHolidaysAsyncTest(int id)
    {
        Assert.IsNull(await _dao.GetHolidayByIdAsync(id));
    }

    [Test(Description="Gets all holidays by year")]
    public async Task GetHolidaysByYearAsyncTest()
    {
        var result = await _dao.GetHolidaysByYearAsync(2024);
        Assert.That(result.Count(), Is.EqualTo(24));
    }
}