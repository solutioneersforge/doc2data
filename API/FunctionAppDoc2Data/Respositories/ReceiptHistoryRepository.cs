using FunctionAppDoc2Data.DataContext;
using FunctionAppDoc2Data.Mappers;
using FunctionAppDoc2Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.Respositories;
public class ReceiptHistoryRepository : IReceiptHistoryRepository
{
    private readonly DocToDataDBContext _docToDataDBContext;
    public ReceiptHistoryRepository(DocToDataDBContext docToDataDBContext)
    {
        _docToDataDBContext = docToDataDBContext;
    }

    public IEnumerable<ReceiptHistoryDTO> GetReceiptHistory()
    {
        var resultReceipt = _docToDataDBContext.Receipts.AsNoTracking().Include(m => m.Merchant).Include(m => m.Status).ToList();
        return resultReceipt.MapToReceiptHistory();
    }
}
