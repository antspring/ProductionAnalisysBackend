using Domain.Models.ProductionAnalysis.HourlySeveral;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityTypeConfigurations;

public class HourlySeveralViewConfiguration : IEntityTypeConfiguration<HourlySeveralView>
{
    public void Configure(EntityTypeBuilder<HourlySeveralView> builder)
    {
        builder.ToView("HourlySeveralView");
        builder.HasNoKey();
    }
}