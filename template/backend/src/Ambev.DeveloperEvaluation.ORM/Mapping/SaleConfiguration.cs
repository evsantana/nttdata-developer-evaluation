using Ambev.DeveloperEvaluation.Domain.Models.SaleCase.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales");

            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(u => u.SaleNumber).IsRequired().HasMaxLength(50);
            builder.Property(u => u.SaleDate).IsRequired();
            builder.Property(u => u.Branch).IsRequired().HasMaxLength(150);
            builder.Property(u => u.TotalAmount).IsRequired().HasColumnType("decimal(18,2)");

            builder.HasMany(c => c.SaleItems)
                   .WithOne(ci => ci.Sale)
                   .HasForeignKey(ci => ci.SaleId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
