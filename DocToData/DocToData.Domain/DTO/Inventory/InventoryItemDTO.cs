using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocToData.Domain.DTO.Inventory
{
    public class InventoryItemDTO
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string Active { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
