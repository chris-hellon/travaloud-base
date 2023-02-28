using System;
using Microsoft.AspNetCore.Identity;

namespace Travaloud.Core.Models
{
	public class ApplicationUserRole : IdentityUserRole<string>
    {
		public string TenantId { get; set; }

		public ApplicationUserRole()
		{

		}

        public ApplicationUserRole(string tenantId, string roleId, string userId) 
		{
			RoleId = roleId;
			UserId = userId;
			TenantId = tenantId;
		}
	}
}

