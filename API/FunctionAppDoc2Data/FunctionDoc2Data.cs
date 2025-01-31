using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
                            HttpClient httpClient = new HttpClient();
                            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "nZ3429AEVqS2ilqBniaYQEIVIiegv3JF6iCZe9OMDfNwOLoYUv1rJQQJ99BAACYeBjFXJ3w3AAALACOGUHKc");

                            HttpResponseMessage responseGet = await httpClient.GetAsync($"https://doc2data.cognitiveservices.azure.com/documentintelligence/documentModels/doc2datav2/analyzeResults/{requestId}?api-version=2024-11-30");
                            if (responseGet.IsSuccessStatusCode)
                            {
                                string responseBodyGet = await responseGet.Content.ReadAsStringAsync();
                                var data = System.Text.Json.JsonSerializer.Deserialize<Rootobject>(responseBodyGet);
                                return new OkObjectResult(new
                                {
                                    data = ReceiptItemParseData.GetReceiptDetails(data)
                                });
                            }
                        }
                        else
                        {

                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            return new OkObjectResult(new
            {
                //Message = "File uploaded successfully",
                //FileName = file.FileName,
                //Size = file.Length,
                //Content = fileContent.Substring(0, Math.Min(100, fileContent.Length)) // Truncated preview
            });
        }
    }
}
