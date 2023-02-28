using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Travaloud.Core.Data;
using Travaloud.Core.Entities.Catalog;
using Travaloud.Core.Interfaces;
using Travaloud.Core.Interfaces.Repositories;
using Travaloud.Core.Interfaces.Services;
using Travaloud.Core.Models.WebComponents;

namespace Travaloud.Core.Models
{
	public abstract class TravaloudBasePageModel : PageModel
	{
        #region Injected Dependencies
        private IConfiguration _configuration;
        protected IConfiguration Configuration => _configuration ??= HttpContext.RequestServices.GetService<IConfiguration>();

        private IHttpContextAccessor _httpContextAccessor;
        protected IHttpContextAccessor HttpContextAccessor => _httpContextAccessor ??= HttpContext.RequestServices.GetService<IHttpContextAccessor>();

        private IErrorLoggerService _errorLoggerService;
        protected IErrorLoggerService ErrorLoggerService => _errorLoggerService ??= HttpContext.RequestServices.GetService<IErrorLoggerService>();

        private IEmailService _emailService;
        protected IEmailService EmailService => _emailService ??= HttpContext.RequestServices.GetService<IEmailService>();

        private IRazorPartialToStringRenderer _razorPartialToStringRenderer;
        protected IRazorPartialToStringRenderer RazorPartialToStringRenderer => _razorPartialToStringRenderer ??= HttpContext.RequestServices.GetService<IRazorPartialToStringRenderer>();

        private IUsersRepository _usersRepository;
        protected IUsersRepository UsersRepository => _usersRepository ??= HttpContext.RequestServices.GetService<IUsersRepository>();

        private IPropertiesRepository _propertiesRepository;
        protected IPropertiesRepository PropertiesRepository => _propertiesRepository ??= HttpContext.RequestServices.GetService<IPropertiesRepository>();

        private IToursRepository _toursRepository;
        protected IToursRepository ToursRepository => _toursRepository ??= HttpContext.RequestServices.GetService<IToursRepository>();

        private IEventsRepository _eventsRepository;
        protected IEventsRepository EventsRepository => _eventsRepository ??= HttpContext.RequestServices.GetService<IEventsRepository>();

        private IBookingsRepository _bookingsRepository;
        protected IBookingsRepository BookingsRepository => _bookingsRepository ??= HttpContext.RequestServices.GetService<IBookingsRepository>();

        private SignInManager<ApplicationUser> _signInManager;
        protected SignInManager<ApplicationUser> SignInManager => _signInManager ??= HttpContext.RequestServices.GetService<SignInManager<ApplicationUser>>();

        private ApplicationUserManager<ApplicationUser> _userManager;
        protected ApplicationUserManager<ApplicationUser> UserManager => _userManager ??= HttpContext.RequestServices.GetService<ApplicationUserManager<ApplicationUser>>();
        #endregion

        #region Properties
        private IConfigurationSection _travaloudConfiguration;
        protected IConfigurationSection TravaloudConfigurtion
        {
            get
            {
                if (_travaloudConfiguration == null)
                    _travaloudConfiguration = Configuration.GetSection("TravaloudConfiguration");

                if (!_travaloudConfiguration.Exists())
                    throw new Exception("No Travaloud Configuration provided in appsettings.json.");

                return _travaloudConfiguration;
            }
        }

        private IConfigurationSection _emailConfiguration;
        protected IConfigurationSection EmailConfiguration
        {
            get
            {
                if (_emailConfiguration == null)
                    _emailConfiguration = Configuration.GetSection("EmailConfig");

                if (!_emailConfiguration.Exists())
                    throw new Exception("No Email Configuration provided in appsettings.json.");

                return _emailConfiguration;
            }
        }

        private string _propertyBookingUrl;
        protected string PropertyBookingUrl
        {
            get
            {
                if (_propertyBookingUrl == null && TravaloudConfigurtion["PropertyBookingUrl"] != null)
                    _propertyBookingUrl = TravaloudConfigurtion["PropertyBookingUrl"];
                else
                    _propertyBookingUrl = "property-booking";

                return _propertyBookingUrl;
            }
        }

        private string _tourBookingUrl;
        protected string TourBookingUrl
        {
            get
            {
                if (_tourBookingUrl == null && TravaloudConfigurtion["TourBookingUrl"] != null)
                    _tourBookingUrl = TravaloudConfigurtion["TourBookingUrl"];
                else
                    _tourBookingUrl = "tour-booking";

                return _tourBookingUrl;
            }
        }

        private string _accountManagementUrl;
        protected string AccountManagementUrl
        {
            get
            {
                if (_accountManagementUrl == null && TravaloudConfigurtion["AccountManagementUrl"] != null)
                    _accountManagementUrl = TravaloudConfigurtion["AccountManagementUrl"];
                else
                    _accountManagementUrl = "my-account/profile";

                return _accountManagementUrl;
            }
        }

        private string _accountManagementImageUrl;
        public string AccountManagementImageUrl
        {
            get
            {
                if (_accountManagementImageUrl == null && TravaloudConfigurtion["AccountManagementImageUrl"] != null)
                    _accountManagementImageUrl = TravaloudConfigurtion["AccountManagementImageUrl"];
                else
                    _accountManagementImageUrl = "https://ik.imagekit.io/rqlzhe7ko/home-page-banner-1.webp?tr=w-2000";

                return _accountManagementImageUrl;
            }
        }

        private string _tenantId;
        protected string TenantId
        {
            get
            {
                if (_tenantId == null)
                {
                    if (TravaloudConfigurtion["TenantId"] != null)
                    {
                        _tenantId = TravaloudConfigurtion["TenantId"];
                        return _tenantId;
                    }

                    throw new Exception("No TenantId provided in appsettings.json.");
                }

                return _tenantId;
            }
        }

        private string _tenantName;
        protected string TenantName
        {
            get
            {
                if (_tenantName == null)
                {
                    if (TravaloudConfigurtion["TenantName"] != null)
                    {
                        _tenantName = TravaloudConfigurtion["TenantName"];
                        return _tenantName;
                    }
                }

                throw new Exception("No TenantName provided in appsettings.json.");
            }
        }

        private string _emailAddress;
        protected string EmailAddress
        {
            get
            {
                if (_emailAddress == null)
                {
                    if (EmailConfiguration["Username"] != null)
                    {
                        _emailAddress = EmailConfiguration["Username"];
                        return _emailAddress;
                    }
                }

                throw new Exception("No Username provided in appsettings.json.");
            }
        }

        private string _emailDisplayName;
        protected string EmailDisplayName
        {
            get
            {
                if (_emailDisplayName == null)
                {
                    if (EmailConfiguration["DisplayName"] != null)
                    {
                        _emailDisplayName = EmailConfiguration["DisplayName"];
                        return _emailDisplayName;
                    }
                }

                throw new Exception("No Label provided in appsettings.json.");
            }
        }

        private string _userId;
        public string UserId
        {
            get
            {
                if (_userId == null)
                    _userId = Helpers.GetUserId(HttpContextAccessor);

                return _userId;
            }
        }

        [TempData]
        public string StatusMessage { get; set; }

        [TempData]
        public string StatusSeverity { get; set; }
        #endregion

        #region Entities
        public IEnumerable<Property> Properties { get; private set; }
        public IEnumerable<Tour> Tours { get; private set; }
        public IEnumerable<Travaloud.Core.Entities.Catalog.Event> Events { get; private set; }
        #endregion

        #region Page Models
        [BindProperty]
        public LoginModalComponent LoginModal { get; set; } = new LoginModalComponent();

        [BindProperty]
        public RegisterModalComponent RegisterModal { get; set; } = new RegisterModalComponent();
        #endregion

        public TravaloudBasePageModel()
        {
            
        }

        public async Task OnGetDataAsync()
        {
            var properties = PropertiesRepository.GetProperties(TenantId);
            var tours = ToursRepository.GetTours(TenantId);
            var events = EventsRepository.GetEvents(TenantId);

            await Task.WhenAll(properties, tours, events);

            Properties = properties.Result;
            Tours = tours.Result;
            Events = events.Result;

            if (StatusSeverity == null)
                StatusSeverity = "success";
        }

        #region Identity
        public async Task<IActionResult> OnPostSignInAsync()
        {
            var returnUrl = LoginModal.ReturnUrl;
            var result = await SignInManager.PasswordSignInAsync(LoginModal.Email, LoginModal.Password, LoginModal.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var userId = Helpers.GetUserId(HttpContextAccessor);

                bool localRedirect = returnUrl == null;

                if (returnUrl != null && returnUrl.Contains(PropertyBookingUrl))
                    returnUrl = returnUrl += $"/{userId}";

                returnUrl = returnUrl ?? $"/{AccountManagementUrl}";
                return localRedirect ? LocalRedirect(returnUrl) : Redirect(returnUrl);
            }
            if (result.RequiresTwoFactor)
            {
                returnUrl = $"/login-with-2fa{(returnUrl != null ? $"?returnUrl={returnUrl}" : "")}";
                return LocalRedirect(returnUrl);
            }
            if (result.IsLockedOut)
            {
                return RedirectToPage("/lockout");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostRegisterAsync()
        {
            var user = new ApplicationUser()
            {
                FirstName = RegisterModal.FirstName,
                LastName = RegisterModal.Surname,
                PhoneNumber = RegisterModal.PhoneNumber,
                Gender = RegisterModal.Gender,
                Nationality = RegisterModal.Nationality,
                DateOfBirth = RegisterModal.DateOfBirth,
                UserName = RegisterModal.Email,
                Email = RegisterModal.Email,
                SignUpDate = DateTime.Now,
                TenantId = _tenantId,
                IsActive = true,
                RefreshTokenExpiryTime = DateTime.Now
            };

            var result = await UserManager.CreateAsync(user, RegisterModal.Password);

            if (result.Succeeded)
            {
                var roleId = await UsersRepository.GetRoleId("Guest");

                if (roleId != null)
                {
                    var userResult = await UserManager.AddToRoleByRoleIdAsync(user, roleId);

                    if (userResult.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false);

                        var returnUrl = RegisterModal.ReturnUrl;
                        var localRedirect = returnUrl == null;

                        if (returnUrl != null && returnUrl.Contains(PropertyBookingUrl))
                            returnUrl = returnUrl += $"/{user.Id}";

                        returnUrl = returnUrl ?? $"/{AccountManagementUrl}";
                        return localRedirect ? LocalRedirect(returnUrl) : Redirect(returnUrl);
                    }
                }
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }
        #endregion


        public async Task<IActionResult> OnPostSearchRooms()
        {
            string userId = Helpers.GetUserId(HttpContextAccessor);

            var propertyId = GetFormValue("PropertyId");
            var checkInDate = GetFormValue("CheckInDate");
            var checkOutDate = GetFormValue("CheckOutDate");

            if (propertyId != null && checkInDate != null && checkOutDate != null)
            {
                Guid propertyIdParsed = Guid.Parse(propertyId);
                DateTime checkInDateParsed = DateTime.Parse(checkInDate);
                DateTime checkOutDateParsed = DateTime.Parse(checkOutDate);

                var property = await PropertiesRepository.GetProperty(TenantId, propertyIdParsed);
                var propertyName = property.FriendlyUrl;

                var url = $"/{PropertyBookingUrl}/{propertyName}/{checkInDateParsed.ToString("yyyy-MM-dd").UrlFriendly()}/{checkOutDateParsed.ToString("yyyy-MM-dd").UrlFriendly()}{(userId != null ? $"/{userId}" : "")}";

                return LocalRedirect(url);
            }

            return LocalRedirect("/error/404");
        }

        #region Ajax Methods
        public async Task<JsonResult> OnPostValidateUser([FromBody] ValidateUserDTO model)
        {
            string message = "<p><strong>We were unable to find a user with the details provided.</strong></p><p>Please ensure your details are correct, or Contact Us</p>";

            try
            {
                var user = await UserManager.FindByEmailAsync(model.Username);

                if (user != null)
                {
                    var passwordCorrect = await UserManager.CheckPasswordAsync(user, model.Password);

                    if (passwordCorrect)
                        return new JsonResult(new
                        {
                            Success = true,
                            Message = ""
                        });
                    else message = "<p><strong>Your password is incorrect.</strong></p><p>Please ensure you've inserted the correct password, or reset your password.</p>";
                }
            }
            catch (Exception e)
            {
                ErrorLoggerService.LogError(e);
            }

            return new JsonResult(new
            {
                Success = false,
                Message = message
            });
        }

        public async Task<JsonResult> OnPostCheckIfUserExists([FromBody] ValidateUserDTO model)
        {
            string message = "<p><strong>A user with this Email Address already exists.</strong></p><p>Please choose a different Email Address.</p>";

            try
            {
                var user = await UserManager.FindByEmailAsync(model.Username);

                if (user == null)
                {
                    bool passwordOk = true;
                    message = "<p><strong>Password validation failed.</strong>";

                    foreach (IPasswordValidator<ApplicationUser> passwordValidator in UserManager.PasswordValidators)
                    {
                        var result = await passwordValidator.ValidateAsync(UserManager, user, model.Password);

                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                message += $"<p>{error.Description}</p>";
                            }

                            passwordOk = false;
                        }
                    }

                    if (passwordOk)
                        return new JsonResult(new
                        {
                            Success = true,
                            Message = ""
                        });
                }
            }
            catch (Exception e)
            {
                ErrorLoggerService.LogError(e);
            }

            return new JsonResult(new
            {
                Success = false,
                Message = message
            });
        }

        public async Task<IActionResult> OnPostCreatePropertyBooking([FromBody] Core.DTOs.Booking booking)
        {
            try
            {
                var bookingInsert = await BookingsRepository.CreateBooking(booking.Description, booking.TotalAmount, booking.CurrencyCode, booking.ItemQuantity, booking.IsPaid, TenantId, UserId);

                if (booking.Items.Any())
                {
                    foreach (var item in booking.Items)
                    {
                        var itemInsert = await BookingsRepository.CreateBookingItem(bookingInsert.Id, item.StartDate, item.EndDate, item.Amount, item.RoomQuantity.Value, item.PropertyId.Value, item.CloudbedsReservationId, item.CloudbedsPropertyId.Value, UserId);

                        if (item.Rooms.Any())
                        {
                            foreach (var room in item.Rooms)
                            {
                                await BookingsRepository.CreateBookingItemRoom(itemInsert.Id, room.RoomName, room.Amount, room.Nights, room.CheckInDate, room.CheckOutDate, room.GuestFirstName, room.GuestLastName, room.CloudbedsGuestId, UserId);
                            }
                        }
                    }
                }

                return new JsonResult(booking);
            }
            catch (Exception e)
            {
                ErrorLoggerService.LogError(e, booking);
            }

            return new JsonResult("fail");
        }
        #endregion

        private string GetFormValue(string key)
        {
            string returnValue = "";
            var parentKeys = new string[] { "CarouselComponent", "BookNowBanner" };

            foreach (var parentKey in parentKeys)
            {
                var keyValue = $"{parentKey}.{key}";
                if (Request.Form.ContainsKey($"{parentKey}.{key}"))
                    returnValue = Request.Form[keyValue].ToString();
            }

            if (returnValue == "" && Request.Form.ContainsKey(key))
                returnValue = Request.Form[key].ToString();

            return returnValue;
        }
    }
}

