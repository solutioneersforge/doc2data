using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FunctionAppDoc2Data
{
    public class FunctionAppCategoryExpenseType
    {
        private readonly IExpenseSubExpenseRepository _expenseSubExpenseRepository;

        public FunctionAppCategoryExpenseType(IExpenseSubExpenseRepository expenseSubExpenseRepository)
        {
            _expenseSubExpenseRepository = expenseSubExpenseRepository;
        }

        [FunctionName("FunctionAppCategoryExpenseType")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                return new OkObjectResult(new
                {
                    Data = _expenseSubExpenseRepository.GetExpenseSubExpenseCategory(),
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
