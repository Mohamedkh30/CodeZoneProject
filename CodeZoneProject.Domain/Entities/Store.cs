namespace CodeZoneProject.Domain.Entities
{
    public class Store : BaseEntity
    {
        public Store() 
        {
            StoreItems = new HashSet<StoreItem>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public virtual ICollection<StoreItem> StoreItems { get; set; }
    }
}
