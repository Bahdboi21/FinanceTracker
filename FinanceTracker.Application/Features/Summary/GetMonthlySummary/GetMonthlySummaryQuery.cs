using FinanceTracker.Application.Dto.RequestModels;
using FinanceTracker.Application.Dto.ResponseModels;
using MediatR;

namespace FinanceTracker.Application.Features.Summary.GetMonthlySummary
{
    public class GetMonthlySummaryQuery : IRequest<ResponseModel<List<SummaryResponse>>>
    {
        public MonthlySummaryRequest Request { get; set; }

        public GetMonthlySummaryQuery(MonthlySummaryRequest request)
        {
            Request = request;
        }
    }
}
