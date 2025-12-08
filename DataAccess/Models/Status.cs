namespace DataAccess.Models;

public class Status
{
    public int Id { get; set; }
    public string Name { get; set; }

    public const int ActiveStatusId = 1;
    public const int InactiveStatusId = 2;
}