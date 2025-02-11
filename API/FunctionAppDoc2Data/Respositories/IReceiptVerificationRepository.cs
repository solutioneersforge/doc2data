using FunctionAppDoc2Data.Models;
using System;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.Respositories;
public interface IReceiptVerificationRepository
{
    ReceiptVerificationMasterDTO GetReceiptVerification(Guid receiptId);
}