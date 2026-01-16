using Domain.Models.Catalogs;
using Domain.Models.ProductionAnalysis.HourlySeveral;
using Domain.Models.ProductionDownTime;

namespace Application.DTO.Responses.HourlySeveral;

public class HourlySeveralResponse
{
    public int Id { get; set; }
    public CatalogValue Product1 { get; set; }
    public CatalogValue Product2 { get; set; }
    public CatalogValue Department { get; set; }
    public CatalogValue Performer { get; set; }
    public CatalogValue Shift { get; set; }
    public CatalogValue WorkHour { get; set; }

    public double CycleTime1 { get; set; }
    public double CycleTime2 { get; set; }
    public double DailyRate1 { get; set; }
    public double DailyRate2 { get; set; }

    public double Fact { get; set; }
    public double Changeover { get; set; }

    public ProductionDocument ProductionDocument { get; set; } = null!;

    public double Plan { get; set; }
    public double PlanCumulative { get; set; }
    public double FactCumulative { get; set; }
    public double Deviation { get; set; }
    public double DeviationCumulative { get; set; }
    public double TotalFact { get; set; }
    public double TotalPlan { get; set; }

    public HourlySeveralResponse(HourlySeveralView hourlySeveralView)
    {
        Id = hourlySeveralView.Id;
        Product1 = hourlySeveralView.Product1;
        Product2 = hourlySeveralView.Product2;
        Department = hourlySeveralView.Department;
        Performer = hourlySeveralView.Performer;
        Shift = hourlySeveralView.Shift;
        WorkHour = hourlySeveralView.WorkHour;
        CycleTime1 = hourlySeveralView.CycleTime1;
        CycleTime2 = hourlySeveralView.CycleTime2;
        DailyRate1 = hourlySeveralView.DailyRate1;
        DailyRate2 = hourlySeveralView.DailyRate2;
        Fact = hourlySeveralView.Fact;
        Changeover = hourlySeveralView.Changeover;
        ProductionDocument = hourlySeveralView.ProductionDocument;
        Plan = hourlySeveralView.Plan;
        PlanCumulative = hourlySeveralView.PlanCumulative;
        FactCumulative = hourlySeveralView.FactCumulative;
        Deviation = hourlySeveralView.Deviation;
        DeviationCumulative = hourlySeveralView.DeviationCumulative;
        TotalFact = hourlySeveralView.TotalFact;
        TotalPlan = hourlySeveralView.TotalPlan;
    }
}