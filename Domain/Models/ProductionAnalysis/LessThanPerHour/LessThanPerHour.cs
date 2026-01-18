using Domain.Models.Catalogs;
using Domain.Models.ProductionDownTime;

namespace Domain.Models.ProductionAnalysis.LessThanPerHour;

public class LessThanPerHour
{
    public int Id { get; set; }

    public int DepartmentId { get; set; }
    public CatalogValue Department { get; set; } = null!;

    public int PerformerId { get; set; }
    public CatalogValue Performer { get; set; } = null!;

    public DateOnly Date { get; set; }

    public int ShiftId { get; set; }
    public CatalogValue Shift { get; set; } = null!;

    public int WorkHourId { get; set; }
    public CatalogValue WorkHour { get; set; } = null!;

    public int OperationNameId { get; set; }
    public CatalogValue OperationName { get; set; } = null!;

    public TimeOnly StartTimePlan { get; set; }
    public TimeOnly StartTimeFact { get; set; }
    public TimeOnly EndTimePlan { get; set; }
    public TimeOnly EndTimeFact { get; set; }

    public double Plan { get; set; }
    public double Fact { get; set; }

    public int ProductionDocumentId { get; set; }
    public ProductionDocument ProductionDocument { get; set; } = null!;
    public string Status { get; set; }

    public LessThanPerHour()
    {
    }

    public LessThanPerHour(
        int departmentId,
        int performerId,
        DateOnly date,
        int shiftId,
        int workHourId,
        int operationNameId,
        TimeOnly startTimePlan,
        TimeOnly startTimeFact,
        TimeOnly endTimePlan,
        TimeOnly endTimeFact,
        double plan,
        double fact,
        string status
    )
    {
        DepartmentId = departmentId;
        PerformerId = performerId;
        Date = date;
        ShiftId = shiftId;
        WorkHourId = workHourId;
        OperationNameId = operationNameId;
        StartTimePlan = startTimePlan;
        StartTimeFact = startTimeFact;
        EndTimePlan = endTimePlan;
        EndTimeFact = endTimeFact;
        Plan = plan;
        Fact = fact;
        Status = status;
    }

    public void AddProductionDocument(ProductionDocument productionDocument)
    {
        ProductionDocument = productionDocument;
    }
}