namespace Domain.Models;

public class Status
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public const int ActiveStatusId = 1;
    public const int InactiveStatusId = 2;
}