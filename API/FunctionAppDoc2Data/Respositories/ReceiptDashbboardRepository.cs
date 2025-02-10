﻿using FunctionAppDoc2Data.DataContext;
using FunctionAppDoc2Data.Mappers;
using FunctionAppDoc2Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.Respositories;
public class ReceiptDashbboardRepository : IReceiptDashbboardRepository
{
    private readonly DocToDataDBContext _docToDataDBContext;

    public ReceiptDashbboardRepository(DocToDataDBContext docToDataDBContext)
    {
        _docToDataDBContext = docToDataDBContext;
    }

    public async Task<IEnumerable<ReceiptDashboardDTO>> GetReceiptDashboard()
    {
        try
        {
            var result = _docToDataDBContext.Receipts.Include(m => m.Merchant).OrderByDescending(m => m.CreatedOn).ToList();
            return result.MapToReceiptDashboard();
        }
        catch (Exception ex)
        {
            return Enumerable.Empty<ReceiptDashboardDTO>();
        }
    }
}
