using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Application.Dto.RequestModels
{
    public class LoginRequest
    {
        [EmailAddress]
        public string Email { get; set; } = default!;
        [Required]
        public string Password { get; set; } = default!;
    }
}
