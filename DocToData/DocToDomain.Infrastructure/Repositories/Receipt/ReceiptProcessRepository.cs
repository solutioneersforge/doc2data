using DocToData.Domain.DTO.Helpers;
using DocToData.Domain.Interfaces.Services;
using DocToDomain.Infrastructure.ReceiptProcessor;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocToDomain.Infrastructure.Repositories.Receipt
{
    public class ReceiptProcessRepository : IReceiptProcessRepository
    {
        public Root GetReceiptProcess(IFormFile formFile)
        {
            return ReceiptProcessing.GetReceiptProcessingData(formFile).Result;
        }
    }
}
