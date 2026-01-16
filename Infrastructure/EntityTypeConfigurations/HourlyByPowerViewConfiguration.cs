using Domain.Models.ProductionAnalysis.HourlyByPower;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityTypeConfigurations;

public class HourlyByPowerViewConfiguration : IEntityTypeConfiguration<HourlyByPowerView>
{
    public void Configure(EntityTypeBuilder<HourlyByPowerView> builder)
    {
        builder.ToView("HourlyByPowerView");
        builder.HasNoKey();
    }
}