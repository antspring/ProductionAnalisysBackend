using DataAccess.Models;
using DataAccess.Models.Catalogs;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Status> Statuses { get; set; }
    public DbSet<Catalog> Catalogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationUser>().Property(u => u.StatusId).HasDefaultValue(2);

        modelBuilder.Entity<Status>(s =>
        {
            s.Property(x => x.Name).IsRequired();
            s.HasData(
                new Status { Id = 1, Name = "Active" },
                new Status { Id = 2, Name = "Inactive" });
        });
    }
}