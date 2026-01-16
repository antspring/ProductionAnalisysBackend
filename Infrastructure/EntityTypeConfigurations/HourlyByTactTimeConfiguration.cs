using Domain.Models.ProductionAnalysis.HourlyByTactTime;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityTypeConfigurations;

public class HourlyByTactTimeConfiguration : IEntityTypeConfiguration<HourlyByTactTime>
{
    public void Configure(EntityTypeBuilder<HourlyByTactTime> builder)
    {
        builder.HasIndex(e => new
        {
            e.NameOfProductId,
            e.DepartmentId,
            e.PerformerId,
            e.ShiftId,
            e.WorkHourId,
        }).IsUnique();
    }
}