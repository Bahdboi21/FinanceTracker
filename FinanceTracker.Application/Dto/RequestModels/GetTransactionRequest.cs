namespace FinanceTracker.Application.Dto.RequestModels
{
    public class GetTransactionRequest
    {
        public Guid UserId { get; set; }
        public string? Category { get; set; }
    }
}
