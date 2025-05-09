using FinanceTracker.Domain.Entities;

namespace FinanceTracker.Application.Interfaces
{
    public interface IAuthService
    {
        string GenerateToken(User user);
    }
}
