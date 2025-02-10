using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FunctionAppDoc2Data.Models;
using FunctionAppDoc2Data.Respositories;
using System.Net.Http;
using FunctionAppDoc2Data.Services;

namespace FunctionAppDoc2Data.AzureFunctions
{
    public class FunctionAppCreateReceipt
    {
        private readonly IReceiptRespository _receiptRespository;

        public FunctionAppCreateReceipt(IReceiptRespository receiptRespository)
        {
            _receiptRespository = receiptRespository;
        }

        [FunctionName("FunctionAppCreateReceipt")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                var file = req.Form.Files["file"];
                string filePath = string.Empty;
                if (file != null && file.Length > 0)
                {
                    filePath = await UploadImageToAzure.UploadImage(file);
                }

                using StreamReader reader = new(req.Body);
                string bodyStr = await reader.ReadToEndAsync();
                var receiptMasterStr = req.Form["receiptMasterDTO"];

                var receiptMaster = JsonConvert.DeserializeObject<ReceiptMasterDTO>(receiptMasterStr);
                receiptMaster.ImagePath = filePath;
                receiptMaster.OriginalFileName = file.FileName;

                await _receiptRespository.CreateReceipt(receiptMaster);

                return new OkObjectResult(new
                {
                    Data = "Data Successfully Added",
                    Message = "Success",
                    IsSuccess = true
                });
            }
            catch (Exception ex)
            {
                return new OkObjectResult(new
                {
                    Message = ex.Message.ToString(),
                    IsSuccess = false
                });
            }

        }
    }
}
