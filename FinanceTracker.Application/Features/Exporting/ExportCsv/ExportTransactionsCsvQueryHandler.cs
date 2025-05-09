using FinanceTracker.Application.Dto.ResponseModels;
using FinanceTracker.Application.Interfaces;
using FinanceTracker.Domain.Entities;
using MediatR;
using System.Text;

namespace FinanceTracker.Application.Features.Exporting.ExportCsv
{
    public class ExportTransactionsCsvQueryHandler : IRequestHandler<ExportTransactionsCsvQuery, ExportFileResultResponse>
    {
        private readonly IRepository<Transaction> _repo;

        public ExportTransactionsCsvQueryHandler(IRepository<Transaction> repo)
        {
            _repo = repo;
        }

        public async Task<ExportFileResultResponse> Handle(ExportTransactionsCsvQuery request, CancellationToken cancellationToken)
        {
            var transactions = await _repo.GetAllBySpec(t => t.UserId == request.Request.UserId);

            var lines = new List<string>
            {
                "Title,Amount,Category,Date"
            };

            lines.AddRange(transactions.Select(t =>
                $"{Escape(t.Title)},{t.Amount},{Escape(t.Category)},{t.Date:yyyy-MM-dd}"
            ));

            var csvBytes = Encoding.UTF8.GetBytes(string.Join("\n", lines));

            return new ExportFileResultResponse
            {
                FileBytes = csvBytes,
                ContentType = "text/csv",
                FileName = "transactions.csv"
            };
        }

        private string Escape(string value)
        {
            if (value.Contains(",") || value.Contains("\""))
                return $"\"{value.Replace("\"", "\"\"")}\"";
            return value;
        }
    }

}
