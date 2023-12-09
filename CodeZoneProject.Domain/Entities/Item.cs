using CodeZoneProject.Domain.Enums;

namespace CodeZoneProject.Domain.Entities
{
    public class Item : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ItemCategory Category { get; set; }
    }
}
