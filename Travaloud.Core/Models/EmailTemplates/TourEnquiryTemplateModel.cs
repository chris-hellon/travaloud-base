using System;
namespace Travaloud.Core.Models.EmailTemplates
{
	public class TourEnquiryTemplateModel : EmailTemplateBaseModel
	{
        public string Name { get; set; }
        public string Tour { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
        public int NumberOfPeople { get; set; }
        public string AdditionalInformation { get; set; }

        public TourEnquiryTemplateModel()
        {

        }

        public TourEnquiryTemplateModel(string tenantName, string primaryBackgroundColor, string secondaryBackgroundColor, string headerBackgroundColor, string textColor, string logoImageUrl, string name, string tour, string email, DateTime date, int numberOfPeople, string additionalInformation) : base(tenantName, primaryBackgroundColor, secondaryBackgroundColor, headerBackgroundColor, textColor, logoImageUrl)
        {
            Name = name;
            Tour = tour;
            Email = email;
            Date = date;
            NumberOfPeople = numberOfPeople;
            AdditionalInformation = additionalInformation;
        }
    }
}

