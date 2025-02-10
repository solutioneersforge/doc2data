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
    public class FunctionAppReceiptDashboard
    {
        private readonly IReceiptDashbboardRepository _receiptDashbboardRepository;
        public FunctionAppReceiptDashboard(IReceiptDashbboardRepository receiptDashbboardRepository)
        {
            _receiptDashbboardRepository = receiptDashbboardRepository;
        }

        [FunctionName("FunctionAppReceiptDashboard")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                return new OkObjectResult(new
                {
                    Data = _receiptDashbboardRepository.GetReceiptDashboard(),
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
