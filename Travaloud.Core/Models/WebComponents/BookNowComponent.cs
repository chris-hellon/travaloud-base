namespace Travaloud.Core.Models.WebComponents
{
	public class BookNowComponent : BaseComponent
    {
        public IEnumerable<Property> Properties { get; private set; }

        [Required(ErrorMessage = "Please select a Hostel")]
        public Guid? PropertyId { get; private set; }

        [Required(ErrorMessage = "Please choose a Check In Date")]
        public DateTime? CheckInDate { get; private set; }

        [Required(ErrorMessage = "Please choose a Check In Date")]
        public DateTime? CheckOutDate { get; private set; }

        public int? GuestQuantity { get; private set; }

        public bool Floating { get; private set; }

        public bool IsModal { get; set; }

        public BookNowComponent()
        {

        }

        public BookNowComponent(IEnumerable<Property> properties, Guid? propertyId = null, bool floating = false)
        {
            Properties = properties;
            PropertyId = propertyId;
            Floating = floating;
        }

        public BookNowComponent(Guid propertyId, DateTime? checkInDate, DateTime? checkOutDate, bool floating = false, int? guestQuantity = null)
        {
            PropertyId = propertyId;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            GuestQuantity = guestQuantity;
            Floating = floating;
        }

        public BookNowComponent(bool floating = false)
        {
            Floating = floating;
        }
    }
}

