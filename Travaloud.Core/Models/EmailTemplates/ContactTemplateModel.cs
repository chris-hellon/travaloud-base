using System;
namespace Travaloud.Core.Models.EmailTemplates
{
	public class ContactTemplateModel : EmailTemplateBaseModel
	{
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }

        public ContactTemplateModel()
        {

        }

        public ContactTemplateModel(string tenantName, string primaryBackgroundColor, string secondaryBackgroundColor, string headerBackgroundColor, string textColor, string logoImageUrl, string name, string email, string message) : base(tenantName, primaryBackgroundColor, secondaryBackgroundColor, headerBackgroundColor, textColor, logoImageUrl)
        {
            Name = name;
            Email = email;
            Message = message;
        }
    }
}

