using DocToData.Domain.DTO.Receipt;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocToData.Application.Command.Receipt
{
    public record class ReceiptCommand(ReceiptDTO ReceiptDTO) : IRequest<int>;
}
