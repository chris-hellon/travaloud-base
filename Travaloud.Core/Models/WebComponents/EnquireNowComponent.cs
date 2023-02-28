using System;
namespace Travaloud.Core.Models.WebComponents
{
	public class EnquireNowComponent
	{
        public string TourName { get; set; }

        [Required(ErrorMessage = "Please enter a Name.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter an Email Address.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Requested Date")]
        [Required(ErrorMessage = "Please select a Date.")]
        public DateTime? Date { get; set; }

        [Display(Name = "Number of People")]
        [Required(ErrorMessage = "Please enter a Number of People.")]
        public int? NumberOfPeople { get; set; }

        [Display(Name = "Any special requests")]
        public string AdditionalInformation { get; set; }

        [BindProperty]
        [DataType(DataType.EmailAddress)]
        public string HoneyPot { get; set; } = string.Empty;

        public EnquireNowComponent()
        {

        }
    }
}

