using CodeZoneProject.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeZoneProject.Infrastructure.Configurations
{
    public class StoreItemConfiguration : IEntityTypeConfiguration<StoreItem>
    {
        public void Configure(EntityTypeBuilder<StoreItem> builder)
        {
            //builder.ToTable("StoreItems");

            builder.HasKey(x => new { x.ItemId, x.StoreId });

            builder.Property(x => x.ItemId).IsRequired();
            builder.Property(x => x.StoreId).IsRequired();

            builder.HasOne(x => x.Store)
                .WithMany(x => x.StoreItems)
                .HasForeignKey(x => x.StoreId);

            builder.HasOne(x => x.Item)
                .WithMany(x => x.StoreItems)
                .HasForeignKey(x => x.ItemId);
        }
    }
}
