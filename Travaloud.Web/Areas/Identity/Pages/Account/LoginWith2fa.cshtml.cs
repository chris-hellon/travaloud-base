namespace Travaloud.Web.Areas.Identity.Pages.Account
{
    public class LoginWith2faModel : TravaloudBasePageModel
    {
        public LoginWith2faModel()
        {
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(7, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Text)]
            [Display(Name = "Authenticator code")]
            public string TwoFactorCode { get; set; }

            [Display(Name = "Remember this machine")]
            public bool RememberMachine { get; set; }

            public string ReturnUrl { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(bool rememberMe, string returnUrl = null)
        {
            await base.OnGetDataAsync();

            // Ensure the user has gone through the username & password screen first
            var user = await SignInManager.GetTwoFactorAuthenticationUserAsync();

            if (user == null)
            {
                throw new InvalidOperationException($"Unable to load two-factor authentication user.");
            }

            ReturnUrl = returnUrl;
            RememberMe = rememberMe;

            Input = new InputModel()
            {
                ReturnUrl = returnUrl
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(bool rememberMe)
        {
            var returnUrl = Input.ReturnUrl;

            var user = await SignInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new InvalidOperationException($"Unable to load two-factor authentication user.");
            }

            var authenticatorCode = Input.TwoFactorCode.Replace(" ", string.Empty).Replace("-", string.Empty);

            var result = await SignInManager.TwoFactorAuthenticatorSignInAsync(authenticatorCode, rememberMe, Input.RememberMachine);

            var userId = await UserManager.GetUserIdAsync(user);

            if (result.Succeeded)
            {
                bool localRedirect = returnUrl == null;
                returnUrl = returnUrl ?? $"/{AccountManagementUrl}";

                if (returnUrl != null && returnUrl.Contains(PropertyBookingUrl))
                    returnUrl = returnUrl += $"/{userId}";

                return localRedirect ? LocalRedirect(returnUrl) : Redirect(returnUrl);
            }
            else if (result.IsLockedOut)
            {
                return RedirectToPage("/lockout");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid authenticator code.");
                return Page();
            }
        }
    }
}
