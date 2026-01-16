namespace ProductionAnalisysAPI.DTO.Requests.HourlyByTactTime;

public class HourlyByTactTimeCreateRequest
{
    public int NameOfProductId { get; set; }
    public int DepartmentId { get; set; }
    public int PerformerId { get; set; }
    public int ShiftId { get; set; }
    public int WorkHourId { get; set; }
    public int TactTime { get; set; }
    public int DailyRate { get; set; }
    public int Fact { get; set; }
    public DateOnly Date { get; set; }

    public Domain.Models.ProductionAnalysis.HourlyByTactTime.HourlyByTactTime ToModel()
    {
        return new Domain.Models.ProductionAnalysis.HourlyByTactTime.HourlyByTactTime(
            NameOfProductId,
            DepartmentId,
            PerformerId,
            ShiftId,
            WorkHourId,
            TactTime,
            DailyRate,
            Fact,
            Date);
    }
}