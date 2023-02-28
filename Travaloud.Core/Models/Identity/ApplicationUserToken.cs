using System;
using Microsoft.AspNetCore.Identity;

namespace Travaloud.Core.Models
{
	public class ApplicationUserToken : IdentityUserToken<string>
    {
        public string TenantId { get; set; }

        public ApplicationUserToken()
		{
		}
	}
}

