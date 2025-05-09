using FinanceTracker.Domain.Enum;

namespace FinanceTracker.Application.Dto.RequestModels
{
    public class CreateTransactionRequest
    {
        public string Title { get; set; } = default!;
        public decimal Amount { get; set; }
        public string Category { get; set; } = default!;
        public TransactionType Type { get; set; }
        public DateTime Date { get; set; }
    }

}
