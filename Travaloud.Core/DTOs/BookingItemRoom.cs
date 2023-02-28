namespace Travaloud.Core.DTOs
{
    public class BookingItemRoom
    {
        public Guid? Id { get; set; }
        public string RoomName { get; set; }
        public decimal Amount { get; set; } 
        public int Nights { get; set; } 
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string GuestFirstName { get; set; } 
        public string GuestLastName { get; set; }
        public string CloudbedsGuestId { get; set; } 

        public BookingItemRoom()
        {
        }

        public BookingItemRoom(Guid? id, string roomName, decimal amount, int nights, DateTime checkInDate, DateTime checkOutDate, string guestFirstName, string guestLastName, string cloudbedsGuestId)
        {
            Id = id;
            RoomName = roomName;
            Amount = amount;
            Nights = nights;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            GuestFirstName = guestFirstName;
            GuestLastName = guestLastName;
            CloudbedsGuestId = cloudbedsGuestId;
        }
    }
}