using CodeZoneProject.Application.Interfaces;
using CodeZoneProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeZoneProject.Infrastructure
{
    public class Context : DbContext , IContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
        }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<StoreItem> StoreItems { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
