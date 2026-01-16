using Domain.Models;
using Domain.Models.Catalogs;
using Domain.Models.ProductionAnalysis.HourlyByPower;
using Domain.Models.ProductionAnalysis.HourlyByTactTime;
using Domain.Models.ProductionAnalysis.HourlySeveral;
using Domain.Models.ProductionAnalysis.LessThanPerHour;
using Domain.Models.ProductionDownTime;
using Infrastructure.EntityTypeConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Status> Statuses { get; set; }
    public DbSet<Catalog> Catalogs { get; set; }

    public DbSet<ProductionDocument> ProductionDocuments { get; set; }
    public DbSet<ProductionDownTime> ProductionDownTime { get; set; }
    public DbSet<HourlyByTactTime> HourlyByTactTime { get; set; }
    public DbSet<HourlyByTactTimeView> HourlyByTactTimeViews { get; set; }
    public DbSet<HourlyByPower> HourlyByPower { get; set; }
    public DbSet<HourlyByPowerView> HourlyByPowerViews { get; set; }
    public DbSet<HourlySeveral> HourlySeveral { get; set; }
    public DbSet<HourlySeveralView> HourlySeveralViews { get; set; }
    public DbSet<LessThanPerHour> LessThanPerHour { get; set; }
    public DbSet<LessThanPerHourView> LessThanPerHourViews { get; set; }

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

        modelBuilder.ApplyConfiguration(new ProductionDocumentConfiguration());
        modelBuilder.ApplyConfiguration(new HourlyByTactTimeConfiguration());
        modelBuilder.ApplyConfiguration(new HourlyByTactTimeViewConfiguration());
        modelBuilder.ApplyConfiguration(new HourlyByPowerConfiguration());
        modelBuilder.ApplyConfiguration(new HourlyByPowerViewConfiguration());
        modelBuilder.ApplyConfiguration(new HourlySeveralConfiguration());
        modelBuilder.ApplyConfiguration(new HourlySeveralViewConfiguration());
        modelBuilder.ApplyConfiguration(new LessThanPerHourConfiguration());
        modelBuilder.ApplyConfiguration(new LessThanPerHourViewConfiguration());
    }
}