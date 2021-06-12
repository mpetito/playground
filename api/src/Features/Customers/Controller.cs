using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Playground.Features.Customers
{
    [Route("[controller]")]
    public partial class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("")]
        public async Task<ActionResult<GetAll.Result>> GetAll([FromQuery] GetAll.Query query, CancellationToken cancellationToken)
        {
            return await _mediator.Send(query, cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetById.Result>> GetById(int id, CancellationToken cancellationToken)
        {
            return await _mediator.Send(new GetById.Query(id), cancellationToken);
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] Create.Form form, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new Create.Command(form), cancellationToken);
            return CreatedAtAction(nameof(GetById), new { result.Id }, result);
        }

        [HttpPost("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update(int id, [FromBody] Update.Form form, CancellationToken cancellationToken)
        {
            var query = new Update.Query(id);
            var context = await _mediator.Send(query, cancellationToken);

            await _mediator.Send(new Update.Command(form, context), cancellationToken);
            return NoContent();
        }
    }
}
