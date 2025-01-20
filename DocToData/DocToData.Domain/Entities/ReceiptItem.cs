using System;
using System.Collections.Generic;

namespace DocToData.Domain.Entities;

public partial class ReceiptItem
{
    public int ReceiptItemId { get; set; }

    public Guid ReceiptId { get; set; }

    public int ItemId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal Discount { get; set; }

    public decimal SubTotal { get; set; }

    public virtual Item Item { get; set; } = null!;

    public virtual Receipt Receipt { get; set; } = null!;
}
