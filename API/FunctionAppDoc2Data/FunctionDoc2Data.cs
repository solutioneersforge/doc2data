using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace FunctionAppDoc2Data
{
    public  class FunctionDoc2Data
    {
        private readonly IConfiguration _configuration;

        public FunctionDoc2Data(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [FunctionName("FunctionDoc2Data")]
        public  async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string name = _configuration.GetSection("OcpApimSubscriptionKey").Value;
            var formCollection = await req.ReadFormAsync();
            HttpClient _httpClient = new HttpClient();
            if (formCollection != null)
            {
                using (var streamData = new MemoryStream())
                {
                    var file = formCollection.Files[0];
                    await file.CopyToAsync(streamData);
                    byte[] fileBytes = streamData.ToArray();
                    string base64String = Convert.ToBase64String(fileBytes);

                    var requestPayload = new
                    {
                        base64Source = base64String
                    };

                    string jsonPayload = System.Text.Json.JsonSerializer.Serialize(requestPayload);
                    HttpContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");


                    _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _configuration.GetSection("OcpApimSubscriptionKey").Value);

                    try
                    {
                        HttpResponseMessage response = await _httpClient.PostAsync($"{_configuration.GetSection("PostCognitiveServices").Value}", content);

                        if (response.IsSuccessStatusCode)
                        {
                            string requestId = response.Headers.Contains("apim-request-id") ? response.Headers.GetValues("apim-request-id").FirstOrDefault() : "Not Found";

                            string responseBody = await response.Content.ReadAsStringAsync();

                            await Task.Delay(5000);

                            int maxRetryCount = 3;
                            int retryCount = 0;
                            string statusMessage = string.Empty;

                            HttpClient httpClient = new HttpClient();
                            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _configuration.GetSection("OcpApimSubscriptionKey").Value);

                            while (retryCount < maxRetryCount && statusMessage.ToLower() != "succeeded")
                            {
                                string getURL = String.Format(_configuration.GetSection("GetCognitiveServices").Value, requestId);
                                HttpResponseMessage responseGet = await httpClient.GetAsync($"{getURL}");
                                if (responseGet.IsSuccessStatusCode)
                                {
                                    string responseBodyGet = await responseGet.Content.ReadAsStringAsync();
                                    var data = System.Text.Json.JsonSerializer.Deserialize<Rootobject>(responseBodyGet);
                                    statusMessage = data.status;
                                    if (data.status.ToLower() == "succeeded")
                                    {
                                        return new OkObjectResult(new
                                        {
                                            ParseData = ReceiptItemParseData.GetReceiptDetails(data),
                                            Message = "File uploaded successfully",
                                            IsSuccess = true
                                        });
                                    }
                                    await Task.Delay(2000);
                                }
                                retryCount++;
                            }
                        }
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
            return new OkObjectResult(new
            {
                Message = "File uploaded failed",
                IsSuccess = false
            });
        }
    }
}
