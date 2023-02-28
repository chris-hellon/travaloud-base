namespace Travaloud.Core.Entities
{
    public class BookingItem : AuditableEntity, IAggregateRoot
    {
        public Guid? BookingId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public decimal Amount { get; private set; }
        public int? RoomQuantity { get; set; }
        public Guid? PropertyId { get; private set; }
        public Guid? TourId { get; private set; }
        public string CloudbedsReservationId { get; set; }
        public int? CloudbedsPropertyId { get; set; }

        public virtual IEnumerable<BookingItemRoom> Rooms { get; set; } = default!;
    }
}