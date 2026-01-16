namespace Application.DTO.Requests.HourlyByTactTime;

public class HourlyByTactTimeUpdateRequest
{
    public int? NameOfProductId { get; set; }
    public int? DepartmentId { get; set; }
    public int? PerformerId { get; set; }
    public int? ShiftId { get; set; }
    public int? WorkHourId { get; set; }
    public double? TactTime { get; set; }
    public double? DailyRate { get; set; }
    public double? Fact { get; set; }
    public DateOnly? Date { get; set; }
}