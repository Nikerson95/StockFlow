using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockFlow.Domain.Entities;

namespace StockFlow.Infrastructure.Persistence.Configurations;

public class StockMovementConfiguration : IEntityTypeConfiguration<StockMovement>
{
    public void Configure(EntityTypeBuilder<StockMovement> builder)
    {
        builder.ToTable("StockMovements");

        builder.HasKey(movement => movement.Id);

        builder.Property(movement => movement.Type)
            .IsRequired();

        builder.Property(movement => movement.Quantity)
            .IsRequired();

        builder.Property(movement => movement.Reason)
            .HasMaxLength(300);

        builder.Property(movement => movement.CreatedAt)
            .IsRequired();

        builder.Property(movement => movement.ProductId)
            .IsRequired();

        builder.HasOne(movement => movement.Product)
            .WithMany()
            .HasForeignKey(movement => movement.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}