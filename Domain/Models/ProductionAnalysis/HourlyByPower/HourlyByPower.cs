using Domain.Models.Catalogs;
using Domain.Models.ProductionDownTime;

namespace Domain.Models.ProductionAnalysis.HourlyByPower;

public class HourlyByPower
{
    public int Id { get; set; }

    public int NameOfProductId { get; set; }
    public CatalogValue NameOfProduct { get; set; } = null!;

    public int DepartmentId { get; set; }
    public CatalogValue Department { get; set; } = null!;

    public int PerformerId { get; set; }
    public CatalogValue Performer { get; set; } = null!;

    public DateOnly Date { get; set; }

    public int ShiftId { get; set; }
    public CatalogValue Shift { get; set; } = null!;

    public double Power { get; set; }
    public double DailyRate { get; set; }

    public int WorkHourId { get; set; }
    public CatalogValue WorkHour { get; set; } = null!;

    public double Fact { get; set; }

    public int ProductionDocumentId { get; set; }
    public ProductionDocument ProductionDocument { get; set; } = null!;

    public HourlyByPower()
    {
    }

    public HourlyByPower(
        int nameOfProductId,
        int departmentId,
        int performerId,
        int shiftId,
        int workHourId,
        double power,
        double dailyRate,
        double fact,
        DateOnly date)
    {
        NameOfProductId = nameOfProductId;
        DepartmentId = departmentId;
        PerformerId = performerId;
        ShiftId = shiftId;
        WorkHourId = workHourId;
        Power = power;
        DailyRate = dailyRate;
        Fact = fact;
        Date = date;
    }

    public void AddProductionDocument(ProductionDocument productionDocument)
    {
        ProductionDocument = productionDocument;
    }
}