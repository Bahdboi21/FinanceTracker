using FinanceTracker.Application.Dto.RequestModels;
using FinanceTracker.Application.Dto.ResponseModels;
using MediatR;

namespace FinanceTracker.Application.Features.Exporting.ExportCsv
{
    public class ExportTransactionsCsvQuery : IRequest<ExportFileResultResponse>
    {
        public GetUserRequest Request { get; set; }

        public ExportTransactionsCsvQuery(GetUserRequest request)
        {
            Request = request;
        }
    }
}
