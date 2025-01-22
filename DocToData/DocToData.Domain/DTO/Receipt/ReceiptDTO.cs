using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocToData.Domain.DTO.Receipt
{
    public class ReceiptDTO
    {
        public Guid ReceiptId { get; private set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public string ReceiptNumber { get; set; } = String.Empty;
        public DateTime ReceiptDate { get; set; }
        public string MerchantName { get; set; } = String.Empty;
        public string MerchantAddress { get; set; } = String.Empty;
        public string MerchantPhone { get; set; } = String.Empty;
        public string MerchantEmail { get; set;} = String.Empty;
        public string MerchantWebsite { get; set; }  =String.Empty;
        public string MerchantCompanyRegNo { get; set; } = String.Empty;
        public string MerchantTaxCompanyRegNo { get; set; } = String.Empty;
        public int SubCategoryId { get; set; }
        public int? CountryId { get; set; }
        public string CountryCode { get; set; } = String.Empty;
        public int? PaymentTypeId { get; set; }
        public string PaymentType { get; set; } = String.Empty;
        public int? CurrencyId { get; set; }
        public string CurrencyCode { get; set; } = String.Empty;
        public decimal TotalAmount { get; set; } = 0;
        public decimal ServiceCharge { get; set; } = 0;
        public decimal Discount { get; set; } = 0;
        public decimal TaxAmount { get; set; } = 0;
        public decimal SubTotal { get; set; } = 0;
        public required List<ReceiptItemDTO> ReceiptItems { get; set; }
    }
}
