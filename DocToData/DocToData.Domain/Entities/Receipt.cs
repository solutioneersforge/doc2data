using System;
using System.Collections.Generic;

namespace DocToData.Domain.Entities;

public partial class Receipt
{
    public Guid ReceiptId { get; set; }

    public Guid UserId { get; set; }

    public string? ReceiptNumber { get; set; }

    public DateTime ReceiptDate { get; set; }

    public int? MerchantId { get; set; }

    public int? CurrencyId { get; set; }

    public decimal? TotalAmount { get; set; }

    public decimal? TaxAmount { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int StatusId { get; set; }

    public virtual Currency? Currency { get; set; }

    public virtual ICollection<ReceiptCategory> ReceiptCategories { get; set; } = new List<ReceiptCategory>();

    public virtual ICollection<ReceiptImage> ReceiptImages { get; set; } = new List<ReceiptImage>();

    public virtual ICollection<ReceiptItem> ReceiptItems { get; set; } = new List<ReceiptItem>();

    public virtual Status Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
