using DocToData.Domain.DTO.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocToData.Domain.Interfaces.Services
{
    public interface IReceiptProcessRepository
    {
        Root GetReceiptProcess(IFormFile formFile);
    }
}
