using System.Collections.Generic;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FunctionAppDoc2Data;
public class ReceiptItemParseData
{
    public static ReceiptDetails GetReceiptDetails(Rootobject rootobject)
    {
        var docs = rootobject.analyzeResult.documents;
        var doc = docs.FirstOrDefault();
        if (doc is null) return null;
        var receipt = doc.fields;
        var receiptItems = receipt.Items.valueArray;
        ReceiptDetails receiptDetails = new()
        {
            AdditionalFree = receipt.AdditionalFree.valueNumber,
            AmountDue = receipt.AmountDue.valueNumber,
            AmountPaid = receipt.AmountPaid.valueNumber,
            BalanceDue = receipt.BalanceDue.valueNumber,
            BarcodeQRCode = receipt.BarcodeQRCode.valueString,
            Change = receipt.Change.valueNumber,
            Currency = receipt.Currency.valueString,
            CustomerAddress = receipt.CustomerAddress.valueString,
            CustomerName = receipt.CustomerName.valueString,
            CustomerPhone = receipt.CustomerPhone.valueString,
            CustomerTIN = receipt.CustomerTIN.valueString,
            DeliveryInfo = receipt.DeliveryInfo.valueString,
            Discount = receipt.Discount.valueNumber,
            DueDate = receipt.DueDate.valueDate,
            ExchangeRate = receipt.ExchangeRate.valueNumber,
            InvoiceDate = receipt.InvoiceDate.valueDate,
            InvoiceNumber = receipt.InvoiceNumber.valueString,
            IssueDate = receipt.IssueDate.valueDate,
            IssueTime= receipt.IssueTime.valueDate,
            ItemsCount = receipt.ItemsCount.valueNumber,
            OrderNumber = receipt.OrderNumber.valueString,
            PaymentMethod = receipt.PaymentMethod.valueString,
            PaymentReferenceNumber = receipt.PaymentReferenceNumber.valueString,
            PaymentStatus = receipt.PaymentStatus.valueString,
            PaymentTerms = receipt.PaymentTerms.valueString,
            ReturnPolicy = receipt.ReturnPolicy.valueString,
            Server = receipt.Server.valueString,
            ServiceAddress = receipt.ServiceAddress.valueString,
            ServiceEndDate = receipt.ServiceEndDate.valueDate,
            ServiceStartDate = receipt.ServiceStartDate.valueDate,
            ShippingFee = receipt.ShippingFee.valueNumber,
            Station = receipt.Station.valueString,
            Subtotal = receipt.Subtotal.valueNumber,
            Table = receipt.Table.valueString,
            Tax = receipt.Tax.valueNumber,
            TaxableAmount = receipt.TaxableAmount.valueNumber,
            Tip = receipt.Tip.valueNumber,
            TransactionDate = receipt.TransactionDate.valueDate,
            TransactionTime = receipt.TransactionTime.valueDate,
            UnknownField = receipt.UnknownField.valueString,
            VendorAddress = receipt.VendorAddress.valueString,
            VendorEmailAddress = receipt.VendorEmailAddress.valueString,
            VendorName = receipt.VendorName.valueString,
            VendorPhone = receipt.VendorPhone.valueString,
            VendorTIN = receipt.VendorTIN.valueString,
            Total = receipt.Total.valueNumber,
            ReceiptItems = GetReceiptItems(receipt.Items.valueArray)
        };
        return receiptDetails;
    }

    private static List<ReceiptItem> GetReceiptItems(Valuearray[] valueArray)
    {
        List<ReceiptItem> listOfReceiptItems = new List<ReceiptItem>();
        foreach (var item in valueArray)
        {
            var receipt = new ReceiptItem()
            {
                Description = item.valueObject.Description.valueString,
                Quantity = item.valueObject.Quantity.valueNumber,
                TotalPrice = item.valueObject.TotalPrice.valueNumber,
                UnitPrice = item.valueObject.UnitPrice.valueNumber,
                Discount = 0.00M
            };
            listOfReceiptItems.Add(receipt);
        }
        return listOfReceiptItems;
    }
}
