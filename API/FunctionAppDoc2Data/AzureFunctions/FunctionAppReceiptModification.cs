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
using FunctionAppDoc2Data.Enums;

namespace FunctionAppDoc2Data.AzureFunctions
{
    public class FunctionAppReceiptModification
    {
        private readonly IReceiptApprovalRepository _receiptApprovalRepository;

        public FunctionAppReceiptModification(IReceiptApprovalRepository receiptApprovalRepository)
        {
            _receiptApprovalRepository = receiptApprovalRepository;
        }

        [FunctionName("FunctionAppReceiptModification")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                using StreamReader reader = new(req.Body);
                string bodyStr = await reader.ReadToEndAsync();
                
                var receiptMaster = JsonConvert.DeserializeObject<ReceiptApprovalDTO>(bodyStr);
                receiptMaster.StatusId = (int)StatusEnum.OPEN; 
                await _receiptApprovalRepository.CreateUpdateReceiptAndItems(receiptMaster);

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
