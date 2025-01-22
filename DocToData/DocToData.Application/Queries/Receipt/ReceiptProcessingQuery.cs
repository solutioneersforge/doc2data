using DocToData.Domain.DTO.Helpers;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DocToData.Application.Queries.Receipt;
public record class ReceiptProcessingQuery(IFormFile FormFile) : IRequest<Root>;

