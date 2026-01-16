using Domain.Models.ProductionAnalysis.LessThanPerHour;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityTypeConfigurations;

public class LessThanPerHourViewConfiguration : IEntityTypeConfiguration<LessThanPerHourView>
{
    public void Configure(EntityTypeBuilder<LessThanPerHourView> builder)
    {
        builder.ToView("LessThanPerHourView");
        builder.HasNoKey();
    }
}