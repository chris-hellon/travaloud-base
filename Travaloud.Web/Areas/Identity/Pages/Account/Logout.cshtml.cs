namespace Travaloud.Web.Areas.Identity.Pages.Account
{
    public class LogoutModel : TravaloudBasePageModel
    {
        public LogoutModel()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            await SignInManager.SignOutAsync();
            return LocalRedirect("/");
        }
    }
}
