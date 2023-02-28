using System;
using AspNetCore.ReCaptcha;

namespace Travaloud.Core.Models.PageModels
{
	public abstract class TourPageModel : ContactBasePageModel<EmailTemplates.TourEnquiryTemplateModel, EnquireNowComponent>
    {
        [BindProperty]
        public EnquireNowComponent EnquireNowComponent { get; set; }

        public TourPageModel()
		{
		}

        public override async Task<IActionResult> OnGetAsync(string tourName = null)
        {
            await base.OnGetDataAsync();

            EnquireNowComponent = null;
            ModelState.Clear();

            return Page();
        }

        public override Task<IActionResult> OnPostAsync([FromServices] IReCaptchaService service, EmailTemplates.TourEnquiryTemplateModel model, EnquireNowComponent formModel)
        {
            return base.OnPostAsync(service, new EmailTemplates.TourEnquiryTemplateModel(TenantName, PrimaryBackgroundColor(), SecondaryBackgroundColor(), HeaderBackgroundColor(), TextColor(), LogoImageUrl(), EnquireNowComponent.Name, EnquireNowComponent.TourName, EnquireNowComponent.Email, EnquireNowComponent.Date.Value, EnquireNowComponent.NumberOfPeople.Value, EnquireNowComponent.AdditionalInformation), EnquireNowComponent);
        }
    }
}

