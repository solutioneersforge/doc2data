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

namespace FunctionAppDoc2Data
{
    public static class FunctionDoc2Data
    {
        [FunctionName("FunctionDoc2Data")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

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


                    _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "nZ3429AEVqS2ilqBniaYQEIVIiegv3JF6iCZe9OMDfNwOLoYUv1rJQQJ99BAACYeBjFXJ3w3AAALACOGUHKc");

                    try
                    {
                        HttpResponseMessage response = await _httpClient.PostAsync("https://doc2data.cognitiveservices.azure.com/documentintelligence/documentModels/doc2datav2:analyze?_overload=analyzeDocument&api-version=2024-11-30", content);

                        if (response.IsSuccessStatusCode)
                        {
                            string requestId = response.Headers.Contains("apim-request-id") ? response.Headers.GetValues("apim-request-id").FirstOrDefault() : "Not Found";

                            string responseBody = await response.Content.ReadAsStringAsync();

                            await Task.Delay(5000);

                            int maxRetryCount = 3;
                            int retryCount = 0;
                            string statusMessage = string.Empty;

                            HttpClient httpClient = new HttpClient();
                            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "nZ3429AEVqS2ilqBniaYQEIVIiegv3JF6iCZe9OMDfNwOLoYUv1rJQQJ99BAACYeBjFXJ3w3AAALACOGUHKc");

                            while (retryCount < maxRetryCount && statusMessage.ToLower() != "succeeded")
                            {
                                HttpResponseMessage responseGet = await httpClient.GetAsync($"https://doc2data.cognitiveservices.azure.com/documentintelligence/documentModels/doc2datav2/analyzeResults/{requestId}?api-version=2024-11-30");
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
