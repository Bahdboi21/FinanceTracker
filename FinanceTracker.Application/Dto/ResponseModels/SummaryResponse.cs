namespace FinanceTracker.Application.Dto.ResponseModels
{
    public record SummaryResponse
    {
        public string Category { get; set; } = default!;
        public decimal TotalAmount { get; set; }
    }
}
