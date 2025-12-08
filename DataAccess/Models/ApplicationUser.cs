using Microsoft.AspNetCore.Identity;

namespace DataAccess.Models;

public class ApplicationUser : IdentityUser
{
    public int StatusId { get; set; } = Status.InactiveStatusId;
    public Status Status { get; set; }
}