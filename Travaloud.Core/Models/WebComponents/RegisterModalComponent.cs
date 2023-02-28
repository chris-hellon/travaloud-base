using Microsoft.AspNetCore.Mvc.Rendering;

namespace Travaloud.Core.Models.WebComponents
{
	public class RegisterModalComponent
	{
        [Required]
        [Display(Name = "First Name")]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Surname")]
        [MaxLength(100)]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        [MaxLength(256)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Nationality")]
        [MaxLength(10)]
        public string Nationality { get; set; }

        [Required]
        [Display(Name = "Gender")]
        [MaxLength(50)]
        public string Gender { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public SelectList Nationalities { get; set; }

        public string ReturnUrl { get; set; } = null;

        public RegisterModalComponent()
        {
            Nationalities = new SelectList(Helpers.GetNationalities(), "Key", "Value", "");
        }
    }
}

