using CodeZoneProject.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeZoneProject.Application.Items.Models
{
    public class ItemDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ItemCategory Category { get; set; }
        public string CategoryName { get; set; }
    }
}
