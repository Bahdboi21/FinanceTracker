namespace FinanceTracker.Application.Dto.RequestModels
{
    public class MonthlySummaryRequest
    {
        public Guid? UserId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
    }
}
