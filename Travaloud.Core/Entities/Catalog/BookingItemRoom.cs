namespace Travaloud.Core.Entities
{
    public class BookingItemRoom : AuditableEntity, IAggregateRoot
    {
        public Guid? BookingItemId { get; private set; }
        public string RoomName { get; private set; } = default!;
        public decimal Amount { get; private set; } = default!;
        public int Nights { get; private set; } = default!;
        public DateTime CheckInDate { get; private set; }
        public DateTime CheckOutDate { get; private set; }
        public string GuestFirstName { get; private set; } = default!;
        public string GuestLastName { get; private set; } = default!;
        public string CloudbedsGuestId { get; private set; } = default!;

        public BookingItemRoom()
        {
        }
    }
}