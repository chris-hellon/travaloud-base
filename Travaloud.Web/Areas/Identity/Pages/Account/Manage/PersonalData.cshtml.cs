namespace Travaloud.Web.Areas.Identity.Pages.Account.Manage
{
    public class PersonalDataModel : TravaloudBasePageModel
    {
        public PersonalDataModel()
        {

        }

        public async Task<IActionResult> OnGet()
        {
            await base.OnGetDataAsync();

            var user = await UserManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{UserManager.GetUserId(User)}'.");
            }

            return Page();
        }
    }
}
