using Microsoft.AspNetCore.Http.Extensions;
using Travaloud.Core.Models.WebComponents.FuseHostelsAndTravel;

namespace Travaloud.Web.Pages.PropertyBooking
{
	public class IndexModel : TravaloudBasePageModel
    {
        private ApplicationUser _applicationUser { get; set; } = null;
        public string PropertyName { get; set; } = string.Empty;
        public Guid PropertyId { get; set; }
        public string IframeUrl { get; set; }

        [BindProperty]
        public HeaderBannerComponent HeaderBanner { get; private set; }

        public async Task<IActionResult> OnGetAsync(string propertyName, string checkInDate = null, string checkOutDate = null, string userId = null)
        {
            await base.OnGetDataAsync();

            var url = Request.GetEncodedUrl();

            base.LoginModal.ReturnUrl = url;
            base.RegisterModal.ReturnUrl = url;

            if (userId != null)
                _applicationUser = await UserManager.FindByIdAsync(userId);

            var property = Properties.Where(h => h.Name.UrlFriendly() == propertyName.UrlFriendly()).FirstOrDefault();

            if (property != null)
            {
                var pageTitle = property.PageTitle ?? property.Name;
                var pageSubTitle = property.PageSubTitle ?? "";

                HeaderBanner = new HeaderBannerComponent(pageTitle, pageSubTitle, null, property.ImagePath, new List<OvalContainerComponent>() { new OvalContainerComponent("hostelPageHeaderBannerOvals1", 15, null, -30, null) });
                PropertyName = property.Name;
                PropertyId = property.Id;
                IframeUrl = $"https://hotels.cloudbeds.com/reservation/{property.CloudbedsKey}";

                if (_applicationUser != null)
                {
                    IframeUrl += "?";

                    if (!string.IsNullOrEmpty(_applicationUser.FirstName))
                        IframeUrl += $"firstName={_applicationUser.FirstName}";

                    if (!string.IsNullOrEmpty(_applicationUser.LastName))
                        IframeUrl += $"&lastName={_applicationUser.LastName}";

                    if (!string.IsNullOrEmpty(_applicationUser.Email))
                        IframeUrl += $"&email={_applicationUser.Email}";

                    if (!string.IsNullOrEmpty(_applicationUser.Nationality))
                        IframeUrl += $"&country={_applicationUser.Nationality}";

                    if (!string.IsNullOrEmpty(_applicationUser.PhoneNumber))
                        IframeUrl += $"&phone={_applicationUser.PhoneNumber}";
                }

                if (!string.IsNullOrEmpty(checkInDate) || !string.IsNullOrEmpty(checkOutDate))
                {
                    IframeUrl += "#";

                    if (!string.IsNullOrEmpty(checkInDate))
                        IframeUrl += $"&checkin={checkInDate}";

                    if (!string.IsNullOrEmpty(checkOutDate))
                        IframeUrl += $"&checkout={checkOutDate}";
                }

                return Page();
            }

            return LocalRedirect("/error");
        }


    }
}
