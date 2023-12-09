namespace CodeZoneProject.Domain.Entities
{
    public class BaseEntity
    {
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
        public bool Deleted { get; set; }
    }
}
