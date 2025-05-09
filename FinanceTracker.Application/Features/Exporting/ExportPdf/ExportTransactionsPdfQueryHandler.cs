using FinanceTracker.Application.Dto.ResponseModels;
using FinanceTracker.Application.Interfaces;
using FinanceTracker.Domain.Entities;
using MediatR;
using QuestPDF.Fluent;

namespace FinanceTracker.Application.Features.Exporting.ExportPdf
{
    public class ExportTransactionsPdfQueryHandler : IRequestHandler<ExportTransactionsPdfQuery, ExportFileResultResponse>
    {
        private readonly IRepository<Transaction> _repo;

        public ExportTransactionsPdfQueryHandler(IRepository<Transaction> repo)
        {
            _repo = repo;
        }

        public async Task<ExportFileResultResponse> Handle(ExportTransactionsPdfQuery request, CancellationToken cancellationToken)
        {
            var transactions = await _repo.GetAllBySpec(t => t.UserId == request.Request.UserId);

            byte[] pdfBytes = GeneratePdf(transactions);

            return new ExportFileResultResponse
            {
                FileBytes = pdfBytes,
                ContentType = "application/pdf",
                FileName = "transactions.pdf"
            };
        }

        private byte[] GeneratePdf(IEnumerable<Transaction> transactions)
        {
            using var stream = new MemoryStream();

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(20);
                    page.Header().Text("Transaction Report").FontSize(20).Bold().AlignCenter();

                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(3); // Title
                            columns.RelativeColumn(2); // Amount
                            columns.RelativeColumn(3); // Category
                            columns.RelativeColumn(3); // Date
                        });

                        // Header Row
                        table.Header(header =>
                        {
                            header.Cell().Text("Title").Bold();
                            header.Cell().Text("Amount").Bold();
                            header.Cell().Text("Category").Bold();
                            header.Cell().Text("Date").Bold();
                        });

                        // Data Rows
                        foreach (var t in transactions)
                        {
                            table.Cell().Text(t.Title);
                            table.Cell().Text($"{t.Amount:C}");
                            table.Cell().Text(t.Category);
                            table.Cell().Text(t.Date.ToString("yyyy-MM-dd"));
                        }
                    });
                });
            }).GeneratePdf(stream);

            return stream.ToArray();
        }
    }

}
