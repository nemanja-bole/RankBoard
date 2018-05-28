using Microsoft.EntityFrameworkCore;
using RankBoard.Data.ModelBuilders.Identity;
using RankBoard.Data.Models.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RankBoard.Data.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RoleClaim> RoleClaims { get; set; }
        public virtual DbSet<UserClaim> UserClaims { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }
        public virtual DbSet<UserToken> UserTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserModelBuilder());
            builder.ApplyConfiguration(new RoleModelBuilder());

            builder.ApplyConfiguration(new UserClaimModelBulder());
            builder.ApplyConfiguration(new UserLoginModelBuilder());
            builder.ApplyConfiguration(new UserTokenModelBuilder());

            builder.ApplyConfiguration(new RoleClaimBuilder());

            builder.ApplyConfiguration(new UserRoleModelBuilder());


        }
    }
}
