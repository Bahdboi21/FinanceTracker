namespace FinanceTracker.Application.Dto.ResponseModels
{
    public record TransactionResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public decimal Amount { get; set; }
        public string Category { get; set; } = default!;
        public string Type { get; set; } = default!;
        public DateTime Date { get; set; }

    }
}
