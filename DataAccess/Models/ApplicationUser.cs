using Microsoft.AspNetCore.Identity;

namespace DataAccess.Models;

public class ApplicationUser : IdentityUser
{
    public int StatusId { get; set; }
    public Status Status { get; set; }
}