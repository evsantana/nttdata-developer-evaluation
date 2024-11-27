using Ambev.DeveloperEvaluation.Domain.Models.ProductCase.Entities;
using Ambev.DeveloperEvaluation.Domain.Models.SaleCase.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("SaleItems");

            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(u => u.ProductId).IsRequired();
            builder.Property(u => u.Quantity).IsRequired();
            builder.Property(u => u.UnitPrice).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(u => u.TotalPrice).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(u => u.Discount).IsRequired().HasColumnType("decimal(18,2)");

            //Foreing Key 
            builder.HasOne<Product>().WithMany().HasForeignKey(s => s.ProductId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
