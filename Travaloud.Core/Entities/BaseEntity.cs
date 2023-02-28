﻿namespace Travaloud.Core.Entities
{
    public abstract class BaseEntity : BaseEntity<DefaultIdType>
    {
        protected BaseEntity() => Id = NewId.Next().ToGuid();
    }

    public abstract class BaseEntity<TId> : IEntity<TId>
    {
        public TId Id { get; protected set; } = default!;

        public string TenantId { get; protected set; } = default!;

        [NotMapped]
        public List<DomainEvent> DomainEvents { get; } = new();
    }
}

