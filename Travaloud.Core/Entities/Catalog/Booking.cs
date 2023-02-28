namespace Travaloud.Core.Entities
{
    public class Booking : AuditableEntity, IAggregateRoot
    {
        public string Description { get; private set; } = default!;
        public decimal TotalAmount { get; private set; }
        public string CurrencyCode { get; private set; } = default!;
        public int ItemQuantity { get; private set; } = default!;
        public bool IsPaid { get; private set; }
        public DateTime BookingDate { get; private set; }
        public Guid? GuestId { get; private set; }
        public int? InvoiceId { get; private set; }

        public virtual IEnumerable<BookingItem> Items { get; set; } = default!;
    }
}