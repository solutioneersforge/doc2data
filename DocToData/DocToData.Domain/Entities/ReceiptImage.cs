using System;
using System.Collections.Generic;

namespace DocToData.Domain.Entities;

public partial class ReceiptImage
{
    public Guid ReceiptImageId { get; set; }

    public Guid ReceiptId { get; set; }

    public string FileName { get; set; } = null!;

    public string ImagePath { get; set; } = null!;

    public DateTime UploadedDateTime { get; set; }

    public bool IsDelete { get; set; }

    public virtual Receipt Receipt { get; set; } = null!;
}
