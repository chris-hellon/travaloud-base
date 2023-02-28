using Microsoft.AspNetCore.Identity;

namespace Travaloud.Core.Models
{
    public class ApplicationRole : IdentityRole
    {
        public string TenantId { get; set; }

        public string Description { get; set; }

        public ApplicationRole(string name, string description = null, string tenantId = null) : base(name)
        {
            Description = description;
            NormalizedName = name.ToUpperInvariant();
            TenantId = tenantId;
        }
    }
}

