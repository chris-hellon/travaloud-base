namespace Travaloud.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordConfirmation : TravaloudBasePageModel
    {
        public async Task OnGetAsync()
        {
            await base.OnGetDataAsync();
        }
    }
}
