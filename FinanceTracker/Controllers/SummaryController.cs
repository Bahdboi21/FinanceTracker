using FinanceTracker.Application.Dto.RequestModels;
using FinanceTracker.Application.Dto.ResponseModels;
using FinanceTracker.Application.Features.Summary.GetMonthlySummary;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinanceTracker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SummaryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SummaryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("monthly")]
        public async Task<IActionResult> GetMonthlySummary([FromQuery] MonthlySummaryRequest request)
        {
            var result = await _mediator.Send(new GetMonthlySummaryQuery(request));
            return StatusCode(result.StatusCode, result);
        }
    }
}
