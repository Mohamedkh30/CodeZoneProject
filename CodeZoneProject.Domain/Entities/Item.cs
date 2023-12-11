using CodeZoneProject.Domain.Enums;

namespace CodeZoneProject.Domain.Entities
{
    public class Item : BaseEntity
    {
        public Item() 
        {
            StoreItems = new HashSet<StoreItem>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ItemCategory Category { get; set; }
        public virtual ICollection<StoreItem> StoreItems { get; set; }

    }
}
