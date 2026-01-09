namespace DataAccess.DTO.Responses;

public class UserResponse
{
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string Status { get; set; }
    public List<string?> Roles { get; set; }
}