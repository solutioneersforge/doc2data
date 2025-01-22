using DocToData.Domain.DTO.Helpers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Specialized;

namespace DocToDomain.Infrastructure.ReceiptProcessor
{
    public static class ReceiptProcessing
    {
        public static async Task<Root> GetReceiptProcessingData(IFormFile formFile)
        {
            string response = await HttpHelper.HttpPostAsync("https://ocr.asprise.com/api/v1/receipt", 
               new NameValueCollection()
               {
                    {"client_id", "TEST"}, 
                    {"recognizer", "auto"}, 
                    {"ref_no", "ocr_dot_net_123"} 
               },
               new NameValueCollection() { { "file", formFile.FileName } } 
           );
            if (response != null && response == String.Empty)
            {
                var root = JsonConvert.DeserializeObject<Root>(response);
                return root ?? new Root();
            }
            return new Root();
            //if (root?.Receipts != null)
            //{
            //    foreach (var receipt in root.Receipts)
            //    {
            //        Console.WriteLine($"Merchant Name: {receipt.MerchantName}");
            //        Console.WriteLine($"Address: {receipt.MerchantAddress}");
            //        Console.WriteLine($"Phone: {receipt.MerchantPhone}");
            //        Console.WriteLine($"Receipt No: {receipt.ReceiptNo}");
            //        Console.WriteLine($"Date: {receipt.Date}");
            //        Console.WriteLine($"Time: {receipt.Time}");
            //        Console.WriteLine("Items:");
            //        Console.WriteLine("Description\tQty\tAmount");

            //        foreach (var item in receipt.Items)
            //        {
            //            Console.WriteLine($"{item.Description}\t{item.Qty}\t{item.Amount}");
            //        }

            //        Console.WriteLine($"Subtotal: {receipt.Subtotal}");
            //        Console.WriteLine($"Total: {receipt.Total}");
            //        Console.WriteLine(new string('-', 50));
            //    }
            //}
        }
    }
}
