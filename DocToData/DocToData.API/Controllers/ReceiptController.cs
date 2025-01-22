using DocToData.Application.Queries.Inventories;
using DocToData.Application.Queries.Receipt;
using DocToData.Domain.DTO.Receipt;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DocToData.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptController : ControllerBase
    {
        private readonly ILogger<ReceiptController> _logger;
        private readonly IMediator _mediator;

        public ReceiptController(ILogger<ReceiptController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        [HttpPost]
        [Route("ProcessReceiptFile")]
        public async Task<IActionResult> ProcessReceiptFile([FromForm] ProcessReceiptDTO processReceipt, CancellationToken token)
        {
            var result = await _mediator.Send(new ReceiptProcessingQuery(processReceipt.ReceiptFile), token);
            return Ok(result);
        }
    }
}
