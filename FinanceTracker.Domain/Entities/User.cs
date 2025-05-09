using FinanceTracker.Domain.Common;

namespace FinanceTracker.Domain.Entities
{
    public class User  : BaseEntity
    {
        public string Email { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
