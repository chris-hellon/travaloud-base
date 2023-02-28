using Travaloud.Core.Models.WebComponents.FuseHostelsAndTravel;
using AspNetCore.ReCaptcha;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Travaloud.Core.Models.EmailTemplates;

namespace Travaloud.Core.Models.PageModels
{
    public abstract class ContactPageModel : ContactBasePageModel<EmailTemplates.ContactTemplateModel, ContactComponent>
    {
        [BindProperty]
        public ContactComponent ContactComponent { get; set; }

        public override async Task<IActionResult> OnGetAsync(string tourName = null)
        {
            await base.OnGetDataAsync();

            ContactComponent = null;
            ModelState.Clear();

            return Page();
        }

        public override Task<IActionResult> OnPostAsync([FromServices] IReCaptchaService service, EmailTemplates.ContactTemplateModel model, ContactComponent formModel)
        {
            return base.OnPostAsync(service, new EmailTemplates.ContactTemplateModel(TenantName, PrimaryBackgroundColor(), SecondaryBackgroundColor(), HeaderBackgroundColor(), TextColor(), LogoImageUrl(), ContactComponent.Name, ContactComponent.Email, ContactComponent.Message), ContactComponent);
        }
    }
}

