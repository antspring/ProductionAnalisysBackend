using Domain.Models.ProductionAnalysis.HourlyByPower;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityTypeConfigurations;

public class HourlyByPowerConfiguration : IEntityTypeConfiguration<HourlyByPower>
{
    public void Configure(EntityTypeBuilder<HourlyByPower> builder)
    {
        builder.HasIndex(e => new
        {
            e.NameOfProductId,
            e.DepartmentId,
            e.PerformerId,
            e.ShiftId,
            e.WorkHourId,
            e.Date
        }).IsUnique();
    }
}