using System;
using System.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Travaloud.Core.Models;

namespace Travaloud.Core.Data
{
    public class ApplicationUserManager<T> : UserManager<ApplicationUser>
    {
        private readonly UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, ApplicationUserToken, ApplicationRoleClaim> _store;

        public ApplicationUserManager(IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators, IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _store = (UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, ApplicationUserToken, ApplicationRoleClaim>)store;
        }

        public virtual async Task<IdentityResult> AddToRoleByRoleIdAsync(ApplicationUser user, string roleId, string tenant = "fuse")
        {
            ThrowIfDisposed();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (string.IsNullOrWhiteSpace(roleId))
                throw new ArgumentNullException(nameof(roleId));

            _store.Context.Set<ApplicationUserRole>().Add(new ApplicationUserRole(tenant, roleId, user.Id));

            return await UpdateUserAsync(user);
        }
    }
}

