using System;
namespace Travaloud.Core.Interfaces.Repositories
{
	public interface IBookingsRepository
	{
        /// <summary>
        /// Retrieves a list of bookings for a Tenant, or a Guest.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="guestId"></param>
        /// <returns></returns>
        Task<IEnumerable<Booking>> GetBookings(string tenantId, Guid? guestId = null);

        /// <summary>
        /// Creates a Booking.
        /// </summary>
        /// <param name="description"></param>
        /// <param name="totalAmount"></param>
        /// <param name="currencyCode"></param>
        /// <param name="itemQuantity"></param>
        /// <param name="isPaid"></param>
        /// <param name="tenantId"></param>
        /// <param name="createId"></param>
        /// <returns></returns>
        Task<Booking> CreateBooking(string description, decimal totalAmount, string currencyCode, int itemQuantity, bool isPaid, string tenantId, string createId = null);

        /// <summary>
        /// Creates a Booking Item.
        /// </summary>
        /// <param name="bookingId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="amount"></param>
        /// <param name="roomQuantity"></param>
        /// <param name="propertyId"></param>
        /// <param name="cloudbedsReservationId"></param>
        /// <param name="cloudbedsPropertyId"></param>
        /// <param name="createId"></param>
        /// <returns></returns>
        Task<BookingItem> CreateBookingItem(Guid bookingId, DateTime startDate, DateTime endDate, decimal amount, int roomQuantity, Guid propertyId, string cloudbedsReservationId, int cloudbedsPropertyId, string createId = null);

        /// <summary>
        /// Creates a Booking Item Room.
        /// </summary>
        /// <param name="bookingItemId"></param>
        /// <param name="roomName"></param>
        /// <param name="amount"></param>
        /// <param name="nights"></param>
        /// <param name="checkInDate"></param>
        /// <param name="checkOutDate"></param>
        /// <param name="guestFirstName"></param>
        /// <param name="guestLastName"></param>
        /// <param name="cloudbedsGuestId"></param>
        /// <param name="createId"></param>
        /// <returns></returns>
        Task<BookingItemRoom> CreateBookingItemRoom(Guid bookingItemId, string roomName, decimal amount, int nights, DateTime checkInDate, DateTime checkOutDate, string guestFirstName, string guestLastName, string cloudbedsGuestId, string createId = null);

        /// <summary>
        /// Gets all Rooms from a list of Booking Items
        /// </summary>
        /// <param name="bookingItemIds"></param>
        /// <returns></returns>
        Task<IEnumerable<BookingItemRoom>> GetBookingItemsRooms(IEnumerable<Guid> bookingItemIds);
    }
}

