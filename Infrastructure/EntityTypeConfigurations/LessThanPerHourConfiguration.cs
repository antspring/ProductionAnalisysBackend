using Domain.Models.ProductionAnalysis.LessThanPerHour;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityTypeConfigurations;

public class LessThanPerHourConfiguration : IEntityTypeConfiguration<LessThanPerHour>
{
    public void Configure(EntityTypeBuilder<LessThanPerHour> builder)
    {
        builder.HasIndex(e => new
        {
            e.DepartmentId,
            e.PerformerId,
            e.ShiftId,
            e.WorkHourId,
            e.OperationNameId,
            e.Date,
            e.StartTimePlan,
            e.EndTimePlan
        }).IsUnique();
    }
}