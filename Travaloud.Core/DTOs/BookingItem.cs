namespace Travaloud.Core.DTOs
{
    public class BookingItem
    {
        public Guid? Id { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Amount { get; set; }
        public int? RoomQuantity { get; set; }
        public Guid? PropertyId { get; set; }
        public Guid? TourId { get; set; }
        public string CloudbedsReservationId { get; set; }
        public int? CloudbedsPropertyId { get; set; }
        public List<BookingItemRoom> Rooms { get; set; }

        public BookingItem()
        {

        }

        public BookingItem(Guid? id, DateTime startDate, DateTime endDate, decimal amount, int? roomQuantity, Guid? propertyId, Guid? tourId, string cloudbedsReservationId, int? cloudbedsPropertyId, List<BookingItemRoom> rooms)
        {
            Id = id;
            StartDate = startDate;
            EndDate = endDate;
            Amount = amount;
            RoomQuantity = roomQuantity;
            PropertyId = propertyId;
            TourId = tourId;
            CloudbedsReservationId = cloudbedsReservationId;
            CloudbedsPropertyId = cloudbedsPropertyId;
            Rooms = rooms;
        }
    }
}