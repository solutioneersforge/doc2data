using System.Collections.Specialized;
using System.Net.Http.Headers;

namespace DocToDomain.Infrastructure.ReceiptProcessor
{
    public class HttpHelper
    {
        public static async Task<string> HttpPostAsync(string url, NameValueCollection values, NameValueCollection files = null)
        {
            try
            {
                using (var client = new HttpClient())
                using (var formData = new MultipartFormDataContent())
                {
                    foreach (string key in values.Keys)
                    {
                        formData.Add(new StringContent(values[key]), key);
                    }
                    if (files != null)
                    {
                        foreach (string key in files.Keys)
                        {
                            string filePath = files[key];
                            if (File.Exists(filePath))
                            {
                                var fileContent = new ByteArrayContent(File.ReadAllBytes(filePath));
                                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
                                formData.Add(fileContent, key, Path.GetFileName(filePath));
                            }
                            throw new Exception("File not found");
                        }
                    }

                    HttpResponseMessage response = await client.PostAsync(url, formData);
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                return string.Empty;
            }

        }
    }
}
