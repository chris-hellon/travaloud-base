using Travaloud.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Travaloud.Core.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, ApplicationRoleClaim, ApplicationUserToken>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema(SchemaNames.Catalog);

            builder.Entity<ApplicationUser>(entity => {
                entity.ToTable("Users", SchemaNames.Identity);
            });
            builder.Entity<ApplicationRole>(entity => {
                entity.ToTable("Roles", SchemaNames.Identity);
            });
            builder.Entity<ApplicationRoleClaim>(entity => {
                entity.ToTable("RoleClaims", SchemaNames.Identity);
            });
            builder.Entity<ApplicationUserRole>(entity => {
                entity.ToTable("UserRoles", SchemaNames.Identity);
            });
            builder.Entity<IdentityUserClaim<string>>(entity => {
                entity.ToTable("UserClaims", SchemaNames.Identity);
            });
            builder.Entity<IdentityUserLogin<string>>(entity => {
                entity.ToTable("UserLogins", SchemaNames.Identity);
            });
            builder.Entity<ApplicationUserToken>(entity => {
                entity.ToTable("UserTokens", SchemaNames.Identity);
            });

            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }


        public static class SchemaNames
        {
            public static string Auditing = "Auditing"; // "AUDITING";
            public static string Catalog = "Catalog"; // "CATALOG";
            public static string Identity = "Identity"; // "IDENTITY";
            public static string MultiTenancy = "MultiTenancy"; // "MULTITENANCY";
        }
    }

}

