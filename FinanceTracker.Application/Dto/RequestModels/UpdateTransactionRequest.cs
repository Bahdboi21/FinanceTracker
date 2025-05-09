namespace FinanceTracker.Application.Dto.RequestModels
{
    public class UpdateTransactionRequest
    {
        //public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public decimal Amount { get; set; }
        public string Category { get; set; } = default!;
        public DateTime Date { get; set; }
    }
}
