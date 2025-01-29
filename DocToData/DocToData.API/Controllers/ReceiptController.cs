using DocToData.Application.Queries.Inventories;
using DocToData.Application.Queries.Receipt;
using DocToData.Domain.DTO.Receipt;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace DocToData.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptController : ControllerBase
    {
        private readonly ILogger<ReceiptController> _logger;
        private readonly IMediator _mediator;

        public ReceiptController(ILogger<ReceiptController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        [HttpPost]
        [Route("ProcessReceiptFile")]
        public async Task<IActionResult> ProcessReceiptFile([FromForm] ProcessReceiptDTO processReceipt, CancellationToken token)
        {
            //byte[] fileBytes = System.IO.File.ReadAllBytes(processReceipt.ReceiptFile.FileName); // Read file as bytes
            //string base64String = Convert.ToBase64String(fileBytes); // Convert to Base64

            //using (HttpClient client = new HttpClient())
            //{
            //    string url = "https://doc2data.cognitiveservices.azure.com/documentintelligence/documentModels/doc2datav1:analyze?_overload=analyzeDocument&api-version=2024-11-30"; // Replace with your API URL
            //    var requestBody = new { base64Source = base64String }; // Sample JSON object
            //    string jsonBody = System.Text.Json.JsonSerializer.Serialize(requestBody);
            //    HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            //    // Add custom request headers
            //    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "nZ3429AEVqS2ilqBniaYQEIVIiegv3JF6iCZe9OMDfNwOLoYUv1rJQQJ99BAACYeBjFXJ3w3AAALACOGUHKc");
            //    client.DefaultRequestHeaders.Add("Content-Type", "application/json");

            //    try
            //    {
            //        HttpResponseMessage response = await client.PostAsync(url, content);

            //        if (response.IsSuccessStatusCode)
            //        {
            //            string responseBody = await response.Content.ReadAsStringAsync();
            //            Console.WriteLine("Response Body:");
            //            Console.WriteLine(responseBody);

            //            // Read response headers
            //            if (response.Headers.Contains("X-Custom-Response-Header"))
            //            {
            //                string customHeaderValue = response.Headers.GetValues("X-Custom-Response-Header").FirstOrDefault();
            //                Console.WriteLine("Custom Response Header: " + customHeaderValue);
            //            }
            //        }
            //        else
            //        {
            //            Console.WriteLine($"Request failed: {response.StatusCode}");
            //        }`````````````````
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine("Error: " + ex.Message);
            //    }
            //}

            HttpClient _httpClient = new HttpClient();
            using (var formData = new MultipartFormDataContent())
            {
                using (var stream = new MemoryStream())
                {
                    await processReceipt.ReceiptFile.CopyToAsync(stream);
                    byte[] fileBytes = stream.ToArray();
                    string base64String = Convert.ToBase64String(fileBytes);

                    var requestPayload = new
                    {
                        base64Source = base64String
                    };

                    string jsonPayload = JsonSerializer.Serialize(requestPayload);
                    HttpContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                  
                    _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "nZ3429AEVqS2ilqBniaYQEIVIiegv3JF6iCZe9OMDfNwOLoYUv1rJQQJ99BAACYeBjFXJ3w3AAALACOGUHKc");
                    
                    try
                    {
                        HttpResponseMessage response = await _httpClient.PostAsync("https://doc2data.cognitiveservices.azure.com/documentintelligence/documentModels/doc2datav1:analyze?_overload=analyzeDocument&api-version=2024-11-30", content);

                        if (response.IsSuccessStatusCode)
                        {
                            string requestId = response.Headers.Contains("apim-request-id") ? response.Headers.GetValues("apim-request-id").FirstOrDefault(): "Not Found";

                            string responseBody = await response.Content.ReadAsStringAsync();

                            await Task.Delay(5000);
                            HttpClient httpClient = new HttpClient();
                            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "nZ3429AEVqS2ilqBniaYQEIVIiegv3JF6iCZe9OMDfNwOLoYUv1rJQQJ99BAACYeBjFXJ3w3AAALACOGUHKc");

                            HttpResponseMessage responseGet = await httpClient.GetAsync($"https://doc2data.cognitiveservices.azure.com/documentintelligence/documentModels/doc2datav1/analyzeResults/{requestId}?api-version=2024-11-30");
                            if (responseGet.IsSuccessStatusCode)
                            {
                                string responseBodyGet = await responseGet.Content.ReadAsStringAsync();
                            }


                            return Ok(new { Message = "File uploaded successfully!", Data = responseBody });
                        }
                        else
                        {
                            return StatusCode((int)response.StatusCode, "Failed to upload file.");
                        }
                    }
                    catch (Exception ex)
                    {
                        return StatusCode(500, $"Error: {ex.Message}");
                    }
                }
            }

            var result = await _mediator.Send(new ReceiptProcessingQuery(processReceipt.ReceiptFile), token);
            return Ok(result);
        }


    }
}



