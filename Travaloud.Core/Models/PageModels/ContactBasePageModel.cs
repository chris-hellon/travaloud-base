using System.Security.Policy;
using AspNetCore.ReCaptcha;
using Microsoft.AspNetCore.Http.Extensions;
using Travaloud.Core.Models.WebComponents.FuseHostelsAndTravel;

namespace Travaloud.Core.Models.PageModels
{
    [ValidateReCaptcha]
    public abstract class ContactBasePageModel<T, T2> : TravaloudBasePageModel
    {
        public virtual string ToAddress()
        {
            return "";
        }

        public virtual string Subject()
        {
            return "Website Enquiry";
        }

        public virtual string[] BccAddress()
        {
            return new string[] { };
        }

        public virtual string PrimaryBackgroundColor()
        {
            return "#000000";
        }

        public virtual string SecondaryBackgroundColor()
        {
            return "#8c8c8c";
        }

        public virtual string HeaderBackgroundColor()
        {
            return "#000000";
        }

        public virtual string TextColor()
        {
            return "#FFFFFF";
        }

        public virtual string LogoImageUrl()
        {
            return "";
        }

        public virtual string TemplateName()
        {
            return "ContactTemplate";
        }

        public ContactBasePageModel()
		{
		}

        public virtual async Task<IActionResult> OnGetAsync(string tourName = null)
        {
            await base.OnGetDataAsync();

            ModelState.Clear();

            return Page();
        }


        public virtual async Task<IActionResult> OnPostAsync([FromServices] IReCaptchaService service, T model, T2 formModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.Remove("Name");
                ModelState.Remove("Email");
                ModelState.Remove("Message");
                ModelState.Remove("Gender");
                ModelState.Remove("Surname");
                ModelState.Remove("Password");
                ModelState.Remove("FirstName");
                ModelState.Remove("Nationality");
                ModelState.Remove("DateOfBirth");
                ModelState.Remove("PhoneNumber");
                ModelState.Remove("ConfirmPassword");
                ModelState.Remove("Date");
                ModelState.Remove("NumberOfPeople");
            }

            if (!ModelState.IsValid)
            {
                StatusMessage = "<p>Please complete the Google Captcha to send your message.</p>";
                StatusSeverity = "danger";
                return Redirect(Request.GetEncodedUrl());
            }

            if (formModel.GetType().GetProperty("HoneyPot") != null)
            {
                var honeyPotProperty = formModel.GetType().GetProperty("HoneyPot");
                var honeyPotValue = honeyPotProperty.GetValue(formModel, null);
                var honeyPot = honeyPotValue != null ? honeyPotValue.ToString() : null;

                if (!string.IsNullOrEmpty(honeyPot))
                {
                    StatusMessage = "<p>Suspicious activity detected.</p>";
                    StatusSeverity = "danger";
                    return Redirect(Request.GetEncodedUrl());
                }
            }

            var emailHtml = await RazorPartialToStringRenderer.RenderPartialToStringAsync($"/Pages/EmailTemplates/{TemplateName()}.cshtml", model);
            await EmailService.Send(ToAddress(), Subject(), emailHtml, EmailDisplayName, EmailAddress, BccAddress());
            StatusMessage = "<p>Your request has been sent successfully.</p><p>A member of our team will contact you shortly.</p>";

            formModel = default(T2);
            ModelState.Clear();

            return Redirect(Request.GetEncodedUrl());
        }
    }
}

