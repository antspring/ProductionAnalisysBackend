using Domain.Models.ProductionAnalysis.HourlyByTactTime;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityTypeConfigurations;

public class HourlyByTactTimeViewConfiguration : IEntityTypeConfiguration<HourlyByTactTimeView>
{
    public void Configure(EntityTypeBuilder<HourlyByTactTimeView> builder)
    {
        builder.ToView("HourlyByTactTimeView");
        builder.HasNoKey();
    }
}