namespace Application.DTO.Requests.HourlySeveral;

public class HourlySeveralUpdateRequest
{
    public int? Product1Id { get; set; }
    public int? Product2Id { get; set; }
    public int? DepartmentId { get; set; }
    public int? PerformerId { get; set; }
    public int? ShiftId { get; set; }
    public int? WorkHourId { get; set; }
    public DateOnly? Date { get; set; }
    public double? CycleTime1 { get; set; }
    public double? CycleTime2 { get; set; }
    public double? DailyRate1 { get; set; }
    public double? DailyRate2 { get; set; }
    public double? Fact { get; set; }
    public double? Changeover { get; set; }
}