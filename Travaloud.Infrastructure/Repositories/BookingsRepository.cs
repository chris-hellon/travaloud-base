using System;
using Travaloud.Core.Entities;
using Travaloud.Core.Entities.Catalog;
using System.Data;

namespace Travaloud.Infrastructure.Repositories
{
	public class BookingsRepository : BaseRepository, IBookingsRepository
    {
        public BookingsRepository(IDapperContext dapperContext) : base(dapperContext)
        {

        }

        public async Task<IEnumerable<Booking>> GetBookings(string tenantId, Guid? guestId = null)
        {
            var bookings = await DapperConnection.ExecuteGetStoredProcedureList<Booking>("GetBookings", new
            {
                TenantId = tenantId,
                GuestId = guestId
            });

            if (bookings.Any())
            {
                var bookingIds = bookings.Select(x => x.Id);
                var bookingItems = await GetBookingItems(tenantId, bookingIds);

                if (bookingItems.Any())
                {
                    var bookingItemsIds = bookingItems.Select(x => x.Id).ToList();
                    var bookingItemsRooms = await GetBookingItemsRooms(bookingItemsIds);

                    if (bookingItemsRooms.Any())
                    {
                        bookingItems = bookingItems.Select(x => { x.Rooms = bookingItemsRooms.Where(r => r.BookingItemId == x.Id); return x; });
                        bookings = bookings.Select(x => { x.Items = bookingItems.Where(b => b.BookingId == x.Id); return x; });
                    }
                }
                    
            }

            return bookings;
        }

        public async Task<IEnumerable<BookingItem>> GetBookingItems(string tenantId, IEnumerable<Guid> bookingIds)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("Id");

            foreach (var bookingId in bookingIds)
            {
                var dataRow = dataTable.NewRow();
                dataRow["Id"] = bookingId;
                dataTable.Rows.Add(dataRow);
            }

            return await DapperConnection.ExecuteGetStoredProcedureList<BookingItem>("GetBookingItems", new
            {
                TenantId = tenantId,
                BookingIdList = dataTable
            });
        }

        public async Task<IEnumerable<BookingItemRoom>> GetBookingItemsRooms(IEnumerable<Guid> bookingItemIds)
        {
            var dataTable = new DataTable();
            dataTable.Columns.Add("Id");

            foreach (var bookingItemId in bookingItemIds)
            {
                var dataRow = dataTable.NewRow();
                dataRow["Id"] = bookingItemId;
                dataTable.Rows.Add(dataRow);
            }

            return await DapperConnection.ExecuteGetStoredProcedureList<BookingItemRoom>("GetBookingItemsRooms", new
            {
                BookingItemsIdList = dataTable
            });
        }

        public async Task<Booking> CreateBooking(string description, decimal totalAmount, string currencyCode, int itemQuantity, bool isPaid, string tenantId, string createId = null)
        {
            return await DapperConnection.ExecuteGetStoredProcedureSingle<Booking>("CreateOrUpdateBooking", new
            {
                Description = description,
                TotalAmount = totalAmount,
                CurrencyCode = currencyCode,
                ItemQuantity = itemQuantity,
                IsPaid = isPaid,
                GuestId = createId != null ? Guid.Parse(createId) : new Guid?(),
                CreateId = createId != null ? Guid.Parse(createId) : new Guid?(),
                TenantId = tenantId
            });
        }

        public async Task<BookingItem> CreateBookingItem(Guid bookingId, DateTime startDate, DateTime endDate, decimal amount, int roomQuantity, Guid propertyId, string cloudbedsReservationId, int cloudbedsPropertyId, string createId = null)
        {
            return await DapperConnection.ExecuteGetStoredProcedureSingle<BookingItem>("CreateOrUpdateBookingItem", new
            {
                BookingId = bookingId,
                StartDate = startDate,
                EndDate = endDate,
                Amount = amount,
                RoomQuantity = roomQuantity,
                PropertyId = propertyId,
                CloudbedsReservationId = cloudbedsReservationId,
                CloudbedsPropertyId = cloudbedsPropertyId,
                CreateId = createId != null ? Guid.Parse(createId) : new Guid?()
            });
        }

        public async Task<BookingItemRoom> CreateBookingItemRoom(Guid bookingItemId, string roomName, decimal amount, int nights, DateTime checkInDate, DateTime checkOutDate, string guestFirstName, string guestLastName, string cloudbedsGuestId, string createId = null)
        {
            return await DapperConnection.ExecuteGetStoredProcedureSingle<BookingItemRoom>("CreateOrUpdateBookingItemRoom", new
            {
                BookingItemId = bookingItemId,
                RoomName = roomName,
                Amount = amount,
                Nights = nights,
                CheckInDate = checkInDate,
                CheckOutDate = checkOutDate,
                GuestFirstName = guestFirstName,
                GuestLastName = guestLastName,
                CloudbedsGuestId = cloudbedsGuestId,
                CreateId = createId != null ? Guid.Parse(createId) : new Guid?()
            });
        }
    }
}