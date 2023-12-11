using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeZoneProject.Domain.Entities;

namespace CodeZoneProject.Infrastructure.Configurations
{
    public class ItemConfiguration : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("Items");

            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(120).IsRequired();
            builder.Property(x => x.Category).HasConversion<int>().IsRequired();

            builder.Property(b => b.CreationDate).HasColumnType("DATETIME").HasDefaultValueSql("CURRENT_TIMESTAMP()").IsRequired();
            builder.Property(b => b.ModificationDate).HasColumnType("DATETIME").HasDefaultValueSql("CURRENT_TIMESTAMP()");
        }
    }
}
