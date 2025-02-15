﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.Models;
public class ReceiptHistoryDTO
{
    public Guid ReceiptId { get; set; }
    public string ReceiptNumber { get; set; }
    public DateTime ReceiptDate { get; set; }
    public string MerchantName { get; set; }
    public string MerchantAddress { get; set; }
    public string CustomerName { get; set; }
    public string CustomerAddress { get; set; }
    public string CustomerPhone { get; set; }
    public decimal? SubTotalAmount { get; set; }
    public decimal? TotalAmount { get; set; }
    public decimal? TaxAmount { get; set; }
    public decimal? OtherCharge { get; set; }
    public int StatusId { get; set; }
    public string StatusName { get; set; }
    public decimal? ServiceChargeAmount { get; set; }
}
