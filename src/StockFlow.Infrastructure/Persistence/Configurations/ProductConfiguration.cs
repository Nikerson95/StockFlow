using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockFlow.Domain.Entities;

namespace StockFlow.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(product => product.Id);

        builder.Property(product => product.Name)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(product => product.Description)
            .HasMaxLength(500);

        builder.Property(product => product.Price)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(product => product.Quantity)
            .IsRequired();

        builder.Property(product => product.MinimumStock)
            .IsRequired();
            builder.Property(product => product.CategoryId)
    .IsRequired();

builder.HasOne(product => product.Category)
    .WithMany()
    .HasForeignKey(product => product.CategoryId)
    .OnDelete(DeleteBehavior.Restrict);
    }
}