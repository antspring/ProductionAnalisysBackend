using Domain.Models.ProductionDownTime;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityTypeConfigurations;

public class ProductionDocumentConfiguration : IEntityTypeConfiguration<ProductionDocument>
{
    public void Configure(EntityTypeBuilder<ProductionDocument> builder)
    {
        builder.Property(d => d.DocumentType)
            .HasConversion<string>()
            .IsRequired();
    }
}