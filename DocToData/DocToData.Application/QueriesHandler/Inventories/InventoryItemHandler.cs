using DocToData.Application.CustomMapper;
using DocToData.Application.Queries.Inventories;
using DocToData.Application.Results;
using DocToData.Domain.DTO.Inventory;
using DocToData.Domain.Entities;
using DocToData.Domain.Enum;
using DocToData.Domain.Interfaces.Repositories;
using MediatR;
using System.Collections.Generic;

namespace DocToData.Application.QueriesHandler.Inventories
{
    public class InventoryItemHandler : IRequestHandler<InventoryItemQuery, CustomResult<IEnumerable<InventoryItemDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public InventoryItemHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<CustomResult<IEnumerable<InventoryItemDTO>>> Handle(InventoryItemQuery request, CancellationToken cancellationToken)
        {
            var result = _unitOfWork.Repository<Item>(DBContextEnum.DocToData);
            return await Task.FromResult(CustomResult<IEnumerable<InventoryItemDTO>>.Success(result.GetAll().CreateInventoryItemMapper()));
        }
    }
}
