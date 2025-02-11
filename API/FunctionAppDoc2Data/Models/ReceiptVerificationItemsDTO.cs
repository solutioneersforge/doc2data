using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.Models;
public class ReceiptVerificationItemsDTO
{
    public Guid ReceiptItemID { get; set; }
    public string ItemDescription { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
    public int SubCategoryId { get; set; }
    public string SubCategoryName { get; set; }
}
