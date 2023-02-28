namespace Travaloud.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LockoutModel : TravaloudBasePageModel
    {
        public async Task OnGet()
        {
            await base.OnGetDataAsync();
        }
    }
}
