using Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure;

public class ApplicationUser : IdentityUser
{
    public int StatusId { get; set; } = Status.InactiveStatusId;
    public Status Status { get; set; }
}