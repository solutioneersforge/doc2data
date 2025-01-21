using DocToData.Application.Queries.Inventories;
using DocToData.Domain.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DocToData.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly ILogger<InventoryController> _logger;
        private readonly IMediator _mediator;

        public InventoryController(ILogger<InventoryController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("GetInventoryItems")]
        [ProducesResponseType(typeof(IEnumerable<InventoryItemDTO>), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetInventoryItems(CancellationToken token)
        {
            var result = await _mediator.Send(new InventoryItemQuery(), token);
            return Ok(result);
        }
    }
}
