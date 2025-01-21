using DocToData.Application.CustomMapper;
using DocToData.Application.Queries.Inventories;
using DocToData.Domain.DTO;
using DocToData.Domain.Entities;
using DocToData.Domain.Interfaces.Repositories;
using MediatR;

namespace DocToData.Application.QueriesHandler.Inventories
{
    public class InventoryItemHandler : IRequestHandler<InventoryItemQuery, IEnumerable<InventoryItemDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public InventoryItemHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<InventoryItemDTO>> Handle(InventoryItemQuery request, CancellationToken cancellationToken)
        {
            var result = _unitOfWork.Repository<Item>("DocToData");
            return await Task.FromResult(result.GetAll().CreateInventoryItemMapper());
        }
    }
}
