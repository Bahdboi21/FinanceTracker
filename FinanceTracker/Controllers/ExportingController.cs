using FinanceTracker.Application.Dto.RequestModels;
using FinanceTracker.Application.Features.Exporting.ExportCsv;
using FinanceTracker.Application.Features.Exporting.ExportPdf;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinanceTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExportingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ExportingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("export/csv")]
        public async Task<IActionResult> ExportCsv()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _mediator.Send(new ExportTransactionsCsvQuery(
                     new GetUserRequest { UserId = Guid.Parse(userId) }
            ));

            var fileName = $"transactions_{DateTime.UtcNow:yyyyMMddHHmmss}.csv";
            return File(result.FileBytes, "text/csv", fileName);
        }

        [HttpGet("export/pdf")]
        public async Task<IActionResult> ExportPdf()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var request = new GetUserRequest { UserId = Guid.Parse(userId) };

            var result = await _mediator.Send(new ExportTransactionsPdfQuery(request));

            return File(result.FileBytes, result.ContentType, result.FileName);
        }

    }
}
