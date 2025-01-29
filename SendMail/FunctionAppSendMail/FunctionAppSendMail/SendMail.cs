using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FunctionAppSendMail
{
    public class SendMail
    {
        private readonly ILogger<SendMail> _logger;

        public SendMail(ILogger<SendMail> logger)
        {
            _logger = logger;
        }

        [Function("SendMail")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<Contact>(requestBody);
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult(data.FirstName);
        }
    }

    public class Contact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
    }
}
