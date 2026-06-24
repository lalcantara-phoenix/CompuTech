using CompuTech.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompuTech.Infrastructure.Persistence.Configurations;

public class InventoryItemConfiguration : IEntityTypeConfiguration<InventoryItem>
{
    public void Configure(EntityTypeBuilder<InventoryItem> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.SKU)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(x => x.SKU)
            .IsUnique();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.Property(x => x.Type)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(x => x.Category)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(x => x.SalePrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.StockQuantity)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();
    }
}
