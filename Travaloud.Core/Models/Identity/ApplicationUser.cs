using Microsoft.AspNetCore.Identity;

namespace Travaloud.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string ProfileImageUrl { get; set; } = null;

        public bool EmailSubscribed { get; set; } = true;

        public DateTime? SignUpDate { get; set; }

        public string FirstName { get; set; } = null;

        public string LastName { get; set; } = null;

        public DateTime? DateOfBirth { get; set; }

        public string Gender { get; set; } = null;

        public string Nationality { get; set; } = null;

        public string Address { get; set; } = null;

        public string City { get; set; } = null;

        public string ZipPostalCode { get; set; } = null;

        public string State { get; set; } = null;

        public string PassportNumber { get; set; } = null;

        public DateTime? PassportIssueDate { get; set; }

        public DateTime? PassportExpiryDate { get; set; }

        public string PassportIssuingCountry { get; set; } = null;

        public DateTime? VisaIssueDate { get; set; }

        public DateTime? VisaExpiryDate { get; set; }

        public string TenantId { get; set; }

        public bool IsActive { get; set; }

        public string RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiryTime { get; set; }

        public string ObjectId { get; set; }
    }
}