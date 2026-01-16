namespace ProductionAnalisysAPI.DTO.Requests.LessThanPerHour;

public class LessThanPerHourCreateRequest
{
    public int DepartmentId { get; set; }
    public int PerformerId { get; set; }
    public DateOnly Date { get; set; }
    public int ShiftId { get; set; }
    public int WorkHourId { get; set; }
    public int OperationNameId { get; set; }
    public TimeOnly StartTimePlan { get; set; }
    public TimeOnly StartTimeFact { get; set; }
    public TimeOnly EndTimePlan { get; set; }
    public TimeOnly EndTimeFact { get; set; }

    public double Plan { get; set; }
    public double Fact { get; set; }

    public Domain.Models.ProductionAnalysis.LessThanPerHour.LessThanPerHour ToModel()
    {
        return new(
            DepartmentId,
            PerformerId,
            Date,
            ShiftId,
            WorkHourId,
            OperationNameId,
            StartTimePlan,
            StartTimeFact,
            EndTimePlan,
            EndTimeFact,
            Plan,
            Fact
        );
    }
}