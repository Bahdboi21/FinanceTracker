using FinanceTracker.Application.Dto.RequestModels;
using FinanceTracker.Application.Dto.ResponseModels;
using MediatR;

namespace FinanceTracker.Application.Features.Exporting.ExportPdf
{
    public class ExportTransactionsPdfQuery : IRequest<ExportFileResultResponse>
    {
        public GetUserRequest Request { get; set; }

        public ExportTransactionsPdfQuery(GetUserRequest request)
        {
            Request = request;
        }
    }
}
