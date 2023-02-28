using Microsoft.AspNetCore.Components.Web;
using Travaloud.Core.Models.WebComponents.FuseHostelsAndTravel;

namespace Travaloud.Web.Areas.Identity.Pages.Account.Manage
{
	public class BookingsModel : TravaloudBasePageModel
    {
        [BindProperty]
        public IEnumerable<Booking> Bookings { get; set; }

        [BindProperty]
        public Booking Booking { get; set; }

        [BindProperty]
        public Property BookingProperty { get; set; }

        [BindProperty]
        public FeaturesTableComponent FacilitiesTable { get; private set; }

        [BindProperty]
        public CarouselCardsComponent ToursCards { get; private set; }

        public async Task OnGetAsync(string bookingId = null)
        {
            await base.OnGetDataAsync();

            Bookings = await BookingsRepository.GetBookings(TenantId, Guid.Parse(UserId));

            if (Bookings.Any())
            {
                if (bookingId != null)
                {
                    Booking = Bookings.FirstOrDefault(x => x.Id == Guid.Parse(bookingId));

                    if (Booking != null)
                    {
                        var bookingItemWithProperty = Booking.Items.FirstOrDefault(x => x.PropertyId.HasValue);

                        if (bookingItemWithProperty != null)
                        {
                            BookingProperty = Properties.FirstOrDefault(x => x.Id == bookingItemWithProperty.PropertyId);

                            await PropertiesRepository.GetPropertyInformation(BookingProperty);

                            FacilitiesTable = WebComponentsBuilder.FuseHostelsAndTravel.GetHostelFacilities(BookingProperty);
                            ToursCards = WebComponentsBuilder.FuseHostelsAndTravel.GetToursCarouselCards(Tours, "onScroll", $"TOURS IN {BookingProperty.PageTitle.ToUpper()}", null);
                        }   
                    }
                }  
            }
        }
    }
}


