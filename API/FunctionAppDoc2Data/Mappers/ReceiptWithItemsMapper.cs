using FunctionAppDoc2Data.DataContext;
using FunctionAppDoc2Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.Mappers;
public static class ReceiptWithItemsMapper
{
    public static Receipt MapToReceiptEntity(this Models.ReceiptMasterDTO receiptMaster)
    {
        if(receiptMaster == null)
        {
            throw new ArgumentNullException(nameof(receiptMaster));
        }

        return new Receipt()
        {
            CountryId = 1,
            CreatedOn = DateTime.UtcNow,
            Discount = 0.00M,
            MerchantId = 100,
            OtherCharge = 0.00M,
            PaymentTypeId = 1,
            ReceiptDate = receiptMaster.InvoiceDate,
            ReceiptNumber = receiptMaster.InvoiceNumber,
            ServiceCharge = 0.00M,
            StatusId = 1,
            SubTotal = receiptMaster.SubTotal,
            ReceiptId = Guid.NewGuid(),
            TaxAmount = receiptMaster.TaxAmount,
            TotalAmount = receiptMaster.Total,
            UserId = receiptMaster.UserId,
            CurrencyId = 1,
            CustomerAddress = receiptMaster.CustomerAddress,
            CustomerName = receiptMaster.CustomerName,
            CustomerPhone = receiptMaster.CustomerPhone,
            ReceiptItems = GetReceiptItems(receiptMaster.ReceiptItemDTOs)
        };
    }

    private static ICollection<DataContext.ReceiptItem> GetReceiptItems(List<ReceiptItemDTO> receiptItems)
    {
        if (receiptItems == null) return null;
        return receiptItems.Select(m => new DataContext.ReceiptItem()
        {
            ItemDescription = m.ItemDescription,
            Discount = m.Discount,
            Quantity = m.Quantity,
            SubTotal = m.Total,
            UnitPrice = m.UnitPrice
        }).ToList();
    }
}
