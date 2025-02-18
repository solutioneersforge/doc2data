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

namespace FunctionAppDoc2Data.AzureFunctions
{
    
    public class FunctionAppExpenseType
    {
        private readonly IExpenseTypeRepository _expenseTypeRepository;
        public FunctionAppExpenseType(IExpenseTypeRepository expenseTypeRepository)
        {
            _expenseTypeRepository = expenseTypeRepository;
        }

        [FunctionName("FunctionAppExpenseType")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                using StreamReader reader = new(req.Body);
                string bodyStr = await reader.ReadToEndAsync();
                var receiptMaster = JsonConvert.DeserializeObject<ExpenseTypeDTO>(bodyStr);

                _expenseTypeRepository.UpsertExpenseCategoryAndSubcategory(receiptMaster);

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
