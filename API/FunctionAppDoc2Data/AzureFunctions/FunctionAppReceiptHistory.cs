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

namespace FunctionAppDoc2Data.AzureFunctions
{
    public  class FunctionAppReceiptHistory
    {
        private readonly IReceiptHistoryRepository _receiptHistoryRepository;

        public FunctionAppReceiptHistory(IReceiptHistoryRepository receiptHistoryRepository)
        {
            _receiptHistoryRepository = receiptHistoryRepository;
        }

        [FunctionName("FunctionAppReceiptHistory")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                return new OkObjectResult(new
                {
                    Data = _receiptHistoryRepository.GetReceiptHistory(),
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
