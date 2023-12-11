using CodeZoneProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeZoneProject.Application.Interfaces
{
    public interface IContext
    {
        DbSet<Store> Stores { get; set; }
        DbSet<Item> Items { get; set; }
        DbSet<StoreItem> StoreItems { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
