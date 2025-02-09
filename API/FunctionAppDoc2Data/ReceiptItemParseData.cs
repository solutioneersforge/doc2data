using System.Collections.Generic;
using System.Linq;

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
            AdditionalFree = DocDataHelper.GetNumberFromString(receipt.AdditionalFree.valueNumber, receipt.AdditionalFree.content),
            AmountDue = DocDataHelper.GetNumberFromString(receipt.AmountDue.valueNumber, receipt.AmountDue.content),
            AmountPaid = DocDataHelper.GetNumberFromString(receipt.AmountPaid.valueNumber, receipt.AmountPaid.content),
            BalanceDue = DocDataHelper.GetNumberFromString(receipt.BalanceDue.valueNumber, receipt.BalanceDue.content),
            BarcodeQRCode = receipt.BarcodeQRCode.valueString,
            Change = DocDataHelper.GetNumberFromString(receipt.Change.valueNumber, receipt.Change.content),
            Currency = receipt.Currency.valueString,
            CustomerAddress = receipt.CustomerAddress.valueString,
            CustomerName = receipt.CustomerName.valueString,
            CustomerPhone = receipt.CustomerPhone.valueString,
            CustomerTIN = receipt.CustomerTIN.valueString,
            DeliveryInfo = receipt.DeliveryInfo.valueString,
            Discount = DocDataHelper.GetNumberFromString(receipt.Discount.valueNumber, receipt.Discount.content),
            DueDate = receipt.DueDate.valueDate,
            ExchangeRate = receipt.ExchangeRate.valueNumber,
            InvoiceDate = receipt.InvoiceDate.valueDate.
                    GetFirstNonEmptyDateTime(receipt.TransactionDate.valueDate, receipt.InvoiceDate.valueDate), 
            InvoiceNumber = receipt.InvoiceNumber.content.GetFirstNonEmptyValue(receipt.OrderNumber.content),
            IssueDate = receipt.IssueDate.valueDate,
            IssueTime = receipt.IssueTime.valueDate,
            ItemsCount = (int)DocDataHelper.GetNumberFromString(receipt.ItemsCount.valueNumber, receipt.ItemsCount.content),
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
            Subtotal = DocDataHelper.GetNumberFromString(receipt.Subtotal.valueNumber, receipt.Subtotal.content),
            Table = receipt.Table.valueString,
            Tax = DocDataHelper.GetNumberFromString(receipt.Tax.valueNumber, receipt.Tax.content),
            TaxableAmount = DocDataHelper.GetNumberFromString(receipt.TaxableAmount.valueNumber, receipt.TaxableAmount.content),
            Tip = DocDataHelper.GetNumberFromString(receipt.Tip.valueNumber, receipt.Tip.content),
            TransactionDate = receipt.TransactionDate.valueDate.GetFirstNonEmptyDateTime(receipt.InvoiceDate.valueDate, receipt.IssueDate.valueDate),
            TransactionTime = receipt.TransactionTime.valueDate,
            UnknownField = receipt.UnknownField.valueString,
            VendorAddress = receipt.VendorAddress.valueString,
            VendorEmailAddress = receipt.VendorEmailAddress.valueString,
            VendorName = receipt.VendorName.valueString,
            VendorPhone = receipt.VendorPhone.valueString,
            VendorTIN = receipt.VendorTIN.valueString,
            Total = DocDataHelper.GetNumberFromString(receipt.Total.valueNumber, receipt.Total.content),
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
                Quantity = DocDataHelper.GetNumberFromString(item.valueObject.Quantity.valueNumber, item.valueObject.Quantity.content),
                //TotalPrice = DocDataHelper.GetNumberFromString(item.valueObject.TotalPrice.valueNumber, item.valueObject.TotalPrice.content),
                UnitPrice = DocDataHelper.GetNumberFromString(item.valueObject.UnitPrice.valueNumber, item.valueObject.UnitPrice.content),
                Discount = 0.00M,
                TotalPrice = ValidateTotal(DocDataHelper.GetNumberFromString(item.valueObject.Quantity.valueNumber, item.valueObject.Quantity.content),
                         DocDataHelper.GetNumberFromString(item.valueObject.UnitPrice.valueNumber, item.valueObject.UnitPrice.content), 0)
            };
            listOfReceiptItems.Add(receipt);
        }
        return listOfReceiptItems;
    }

    private static decimal ValidateTotal(decimal quantity, decimal unitPrice, decimal discount) 
    {
       return (quantity * unitPrice) - discount;        
    }
}
