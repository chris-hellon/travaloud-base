namespace Travaloud.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResetPasswordConfirmationModel : TravaloudBasePageModel
    {
        public async Task OnGetAsync()
        {
            await base.OnGetDataAsync();
        }
    }
}
