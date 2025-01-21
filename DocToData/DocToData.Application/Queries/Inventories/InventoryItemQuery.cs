using DocToData.Application.Results;
using DocToData.Domain.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocToData.Application.Queries.Inventories
{
    public record class InventoryItemQuery : IRequest<CustomResult<IEnumerable<InventoryItemDTO>>>;
}
