using Domain.Models.ProductionAnalysis.HourlySeveral;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityTypeConfigurations;

public class HourlySeveralConfiguration : IEntityTypeConfiguration<HourlySeveral>
{
    public void Configure(EntityTypeBuilder<HourlySeveral> builder)
    {
        builder.HasIndex(e => new
        {
            e.DepartmentId,
            e.PerformerId,
            e.ShiftId,
            e.WorkHourId,
            e.Date
        }).IsUnique();
    }
}