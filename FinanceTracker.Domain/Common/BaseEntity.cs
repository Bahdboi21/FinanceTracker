﻿namespace FinanceTracker.Domain.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastModified { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
        public string? DeletedBy { get; set; }
    }
}
