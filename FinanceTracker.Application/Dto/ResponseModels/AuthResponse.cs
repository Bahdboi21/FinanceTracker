namespace FinanceTracker.Application.Dto.ResponseModels
{
    public record AuthResponse
    {
        public string Email { get; set; } = default!;
        public string Token { get; set; } = default!;

    }
}
