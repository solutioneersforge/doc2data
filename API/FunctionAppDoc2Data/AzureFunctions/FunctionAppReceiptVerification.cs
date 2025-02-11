using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using FunctionAppDoc2Data.Respositories;
using FunctionAppDoc2Data.DataContext;

namespace FunctionAppDoc2Data.AzureFunctions
{
    public  class FunctionAppReceiptVerification
    {
        private readonly IReceiptVerificationRepository _receiptVerificationRepository;
        public FunctionAppReceiptVerification(IReceiptVerificationRepository receiptVerificationRepository)
        {
            _receiptVerificationRepository = receiptVerificationRepository;
        }

        [FunctionName("FunctionAppReceiptVerification")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            Guid receiptId = Guid.Parse(req.Query["receiptId"]);
            try
            {
                return new OkObjectResult(new
                {
                    Data = _receiptVerificationRepository.GetReceiptVerification(receiptId),
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
