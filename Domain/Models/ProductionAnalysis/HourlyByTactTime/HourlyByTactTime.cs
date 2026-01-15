using Domain.Models.Catalogs;
using Domain.Models.ProductionDownTime;

namespace Domain.Models.ProductionAnalysis.HourlyByTactTime;

public class HourlyByTactTime
{
    public int Id { get; init; }

    public int NameOfProductId { get; set; }
    public CatalogValue NameOfProduct { get; set; } = null!;

    public int DepartmentId { get; set; }
    public CatalogValue Department { get; set; } = null!;

    public int PerformerId { get; set; }
    public CatalogValue Performer { get; set; } = null!;

    public int ShiftId { get; set; }
    public CatalogValue Shift { get; set; } = null!;

    public int WorkHourId { get; set; }
    public CatalogValue WorkHour { get; set; } = null!;

    public double TactTime { get; set; }
    public double DailyRate { get; set; }
    public double Fact { get; set; }
    public DateOnly Date { get; set; }

    public int ProductionDocumentId { get; set; }
    public ProductionDocument ProductionDocument { get; set; } = null!;

    public HourlyByTactTime()
    {
    }

    public HourlyByTactTime(
        int nameOfProductId,
        int departmentId,
        int performerId,
        int shiftId,
        int workHourId,
        int tactTime,
        int dailyRate,
        int fact,
        DateOnly date)
    {
        NameOfProductId = nameOfProductId;
        DepartmentId = departmentId;
        PerformerId = performerId;
        ShiftId = shiftId;
        WorkHourId = workHourId;
        TactTime = tactTime;
        DailyRate = dailyRate;
        Fact = fact;
        Date = date;
    }
}