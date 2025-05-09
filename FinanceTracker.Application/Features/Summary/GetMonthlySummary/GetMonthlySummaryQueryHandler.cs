using FinanceTracker.Application.Dto.ResponseModels;
using FinanceTracker.Application.Interfaces;
using FinanceTracker.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace FinanceTracker.Application.Features.Summary.GetMonthlySummary
{
    public class GetMonthlySummaryQueryHandler : IRequestHandler<GetMonthlySummaryQuery, ResponseModel<List<SummaryResponse>>>
    {
        private readonly IRepository<Transaction> _repo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetMonthlySummaryQueryHandler(IRepository<Transaction> repo, IHttpContextAccessor httpContextAccessor)
        {
            _repo = repo;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseModel<List<SummaryResponse>>> Handle(GetMonthlySummaryQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
            {
                return ResponseModel<List<SummaryResponse>>.FailResponse("Unauthorized", 401);
            }

            var allTransactions = await _repo.GetAllBySpec(t =>
                t.UserId == Guid.Parse(userId) &&
                t.Date.Month == request.Request.Month &&
                t.Date.Year == request.Request.Year
            );

            var grouped = allTransactions
                .GroupBy(t => t.Category)
                .Select(g => new SummaryResponse
                {
                    Category = g.Key,
                    TotalAmount = g.Sum(x => x.Amount)
                })
                .ToList();

            return ResponseModel<List<SummaryResponse>>.SuccessResponse(grouped, "Monthly summary retrieved successfully", 200);
        }
    }


}
