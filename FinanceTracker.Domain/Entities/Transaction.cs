using FinanceTracker.Domain.Common;
using FinanceTracker.Domain.Enum;

namespace FinanceTracker.Domain.Entities
{
    public class Transaction : BaseEntity
    {
        public Guid UserId { get; set; }
        public string Title { get; set; } = default!;
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; } 
        public string Category { get; set; } = default!;
        public DateTime Date { get; set; }

        public User User { get; set; }
    }

}
