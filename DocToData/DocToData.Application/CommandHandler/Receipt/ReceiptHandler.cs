using DocToData.Application.Command.Receipt;
using MediatR;

namespace DocToData.Application.CommandHandler.Receipt;
public record class ReceiptHandler : IRequestHandler<ReceiptCommand, int>
{
    public Task<int> Handle(ReceiptCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
