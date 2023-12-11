using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeZoneProject.Domain.Entities
{
    public class StoreItem
    {
        public decimal Quantity { get; set; }
        public DateTime EntryDate { get; set; }
        public Guid StoreId { get; set; }
        public Guid ItemId { get; set; }
        public Store Store { get; set; }
        public Item Item { get; set; }
    }
}
