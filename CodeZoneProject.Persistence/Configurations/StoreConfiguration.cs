using CodeZoneProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeZoneProject.Infrastructure.Configurations
{
    public class StoreConfiguration :IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.ToTable("Stores");

            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(120).IsRequired();
            builder.Property(x => x.Address).HasMaxLength(1000).IsRequired();

            builder.Property(i => i.Deleted).HasDefaultValue(false);
            builder.Property(b => b.CreationDate).HasColumnType("DATETIME").HasDefaultValueSql("CURRENT_TIMESTAMP()").IsRequired();
            builder.Property(b => b.ModificationDate).HasColumnType("DATETIME").HasDefaultValueSql("CURRENT_TIMESTAMP()");
        }
    }
}
