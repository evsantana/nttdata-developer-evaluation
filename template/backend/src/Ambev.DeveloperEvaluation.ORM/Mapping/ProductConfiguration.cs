﻿using Ambev.DeveloperEvaluation.Domain.Models.ProductCase.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

            builder.Property(u => u.Title).IsRequired().HasMaxLength(50);
            builder.Property(u => u.Price).IsRequired().HasPrecision(10, 2);
            builder.Property(u => u.Description).IsRequired().HasMaxLength(250);
            builder.Property(u => u.Category).IsRequired().HasMaxLength(250);
            builder.Property(u => u.Image).HasMaxLength(250);
            builder.Property(u => u.Rating).IsRequired();
            builder.Property(u => u.Count).IsRequired();
        }


    }
}
