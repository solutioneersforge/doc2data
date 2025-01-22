using Microsoft.AspNetCore.Http;

namespace DocToData.Domain.DTO.Receipt;

public class ProcessReceiptDTO
{
    public IFormFile ReceiptFile { get; set; }
}

