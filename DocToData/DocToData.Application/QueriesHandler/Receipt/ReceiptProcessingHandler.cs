using DocToData.Application.Queries.Receipt;
using DocToData.Domain.DTO.Helpers;
using DocToData.Domain.Interfaces.Services;
using MediatR;

namespace DocToData.Application.QueriesHandler.Receipt;
public record class ReceiptProcessingHandler : IRequestHandler<ReceiptProcessingQuery, Root>
{
    private readonly IReceiptProcessRepository _receiptProcessRepository;

    public ReceiptProcessingHandler(IReceiptProcessRepository receiptProcessRepository)
    {
        _receiptProcessRepository = receiptProcessRepository;
    }
    public Task<Root> Handle(ReceiptProcessingQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(_receiptProcessRepository.GetReceiptProcess(request.FormFile));
    }
}

