using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.Services;
public static class UploadImageToAzure
{
    public async static Task<string> UploadImage(IFormFile formFile)
    {
        string connectionString = Environment.GetEnvironmentVariable("AZURESTORAGE_CONNECTION_STRING");
        string containerName = Environment.GetEnvironmentVariable("AZURESTORAGE_CONTAINER_NAME");
        string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);

        using (Stream myBlob = formFile.OpenReadStream())
        {
            var blobClient = new BlobContainerClient(connectionString, containerName);
            var blob = blobClient.GetBlobClient(uniqueFileName);
            await blob.UploadAsync(myBlob, overwrite: true);
        }
        return uniqueFileName;
    }
}
