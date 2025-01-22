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

    public int SubCategoryId { get; set; }

    public int CountryId { get; set; }

    public int PaymentTypeId { get; set; }

    public int CurrencyId { get; set; }

    public decimal? TotalAmount { get; set; }

    public decimal? Discount { get; set; }

    public decimal? SubTotal { get; set; }

    public decimal? ServiceCharge { get; set; }

    public decimal? OtherCharge { get; set; }

    public decimal? TaxAmount { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int StatusId { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual Currency Currency { get; set; } = null!;

    public virtual PaymentType PaymentType { get; set; } = null!;

    public virtual ICollection<ReceiptCategory> ReceiptCategories { get; set; } = new List<ReceiptCategory>();

    public virtual ICollection<ReceiptImage> ReceiptImages { get; set; } = new List<ReceiptImage>();

    public virtual ICollection<ReceiptItem> ReceiptItems { get; set; } = new List<ReceiptItem>();

    public virtual Status Status { get; set; } = null!;

    public virtual ExpenseSubCategory SubCategory { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
