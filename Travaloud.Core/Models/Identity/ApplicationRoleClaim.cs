using System;
using Microsoft.AspNetCore.Identity;

namespace Travaloud.Core.Models
{
    public class ApplicationRoleClaim : IdentityRoleClaim<string>
    {
        public string TenantId { get; set; }

        public string CreatedBy { get; init; }

        public DateTime CreatedOn { get; init; }
    }
}

