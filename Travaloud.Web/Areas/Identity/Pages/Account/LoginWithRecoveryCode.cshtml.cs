namespace Travaloud.Web.Areas.Identity.Pages.Account
{
    public class LoginWithRecoveryCodeModel : TravaloudBasePageModel
    {
        public LoginWithRecoveryCodeModel()
        {
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [BindProperty]
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Recovery Code")]
            public string RecoveryCode { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            await base.OnGetDataAsync();

            // Ensure the user has gone through the username & password screen first
            var user = await SignInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new InvalidOperationException($"Unable to load two-factor authentication user.");
            }

            ReturnUrl = returnUrl;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            var user = await SignInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                throw new InvalidOperationException($"Unable to load two-factor authentication user.");
            }

            var recoveryCode = Input.RecoveryCode.Replace(" ", string.Empty);

            var result = await SignInManager.TwoFactorRecoveryCodeSignInAsync(recoveryCode);

            var userId = await UserManager.GetUserIdAsync(user);

            if (result.Succeeded)
            {
                bool localRedirect = returnUrl == null;
                returnUrl = returnUrl ?? $"/{AccountManagementUrl}";

                if (returnUrl != null && returnUrl.Contains(PropertyBookingUrl))
                    returnUrl = returnUrl += $"/{userId}";

                return localRedirect ? LocalRedirect(returnUrl) : Redirect(returnUrl);
            }
            if (result.IsLockedOut)
            {
                return RedirectToPage("./Lockout");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid recovery code entered.");
                return Page();
            }
        }
    }
}
