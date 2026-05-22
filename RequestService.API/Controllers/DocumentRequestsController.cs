using Microsoft.AspNetCore.Mvc;
using RequestService.Application.Dtos;
using RequestService.Application.Services;

namespace RequestService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentRequestsController : ControllerBase
    {
        private readonly IDocumentRequestService _service;

        public DocumentRequestsController(IDocumentRequestService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDocumentRequestDto dto, CancellationToken cancellationToken = default)
        {
            DocumentRequestDto result = await _service.CreateRequestAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            DocumentRequestDto result = await _service.GetRequestByIdAsync(id, cancellationToken);
            return Ok(result);
        }

        [HttpGet("employee/{employeeName}")]
        public async Task<IActionResult> GetByEmployee(string employeeName, CancellationToken cancellationToken = default)
        {
            IReadOnlyList<DocumentRequestDto> result = await _service.GetRequestByEmployeeAsync(employeeName, cancellationToken);
            return Ok(result);
        }

        [HttpPatch("{id:guid}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateStatusDto dto, CancellationToken cancellationToken = default)
        {
            DocumentRequestDto result = await _service.UpdateStatusAsync(id, dto, cancellationToken);
            return Ok(result);
        }
    }
}
