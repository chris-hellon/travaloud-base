using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.WebUtilities;

namespace Travaloud.Web.Areas.Identity.Pages.Account
{
    public class ConfirmEmailChangeModel : TravaloudBasePageModel
    {
        public ConfirmEmailChangeModel()
        {
        }

        public async Task<IActionResult> OnGetAsync(string userId, string email, string code)
        {
            await base.OnGetDataAsync();

            if (userId == null || email == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await UserManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await UserManager.ChangeEmailAsync(user, email, code);
            if (!result.Succeeded)
            {
                StatusMessage = "Error changing email.";
                return Page();
            }

            // In our UI email and user name are one and the same, so when we update the email
            // we need to update the user name.
            var setUserNameResult = await UserManager.SetUserNameAsync(user, email);
            if (!setUserNameResult.Succeeded)
            {
                StatusMessage = "Error changing user name.";
                return Page();
            }

            await SignInManager.RefreshSignInAsync(user);
            StatusMessage = "Thank you for confirming your email change.";
            return Page();
        }
    }
}
