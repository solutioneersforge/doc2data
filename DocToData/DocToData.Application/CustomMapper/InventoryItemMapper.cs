using DocToData.Domain.DTO.Inventory;
using DocToData.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocToData.Application.CustomMapper
{
    public static class InventoryItemMapper
    {
        public static IEnumerable<InventoryItemDTO> CreateInventoryItemMapper(this IEnumerable<Item> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            return items.Select(item => new InventoryItemDTO
            {
                Active = item.IsActive ? "Yes" : "No",
                CreatedOn = item.CreatedOn,
                Description = item.Description ?? string.Empty,
                IsActive = item.IsActive,
                ItemName = item.ItemName,
                ItemId = item.ItemId
            });
        }
    }
}
