using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace RankBoard.Data.Models.Identity
{
    public class User
    {
        [Key]
        [StringLength(450)]
        public string Id { get; set; }
        
        public int AccessFailedCount { get; set; }
        
        public string ConcurrencyStamp { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public bool LockoutEnabled { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }

        [StringLength(256)]
        public string NormalizedEmail { get; set; }

        [StringLength(256)]
        public string NormalizedUserName { get; set; }

        public string PasswordHash { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public string SecurityStamp { get; set; }

        public bool TwoFactorEnabled { get; set; }

        [StringLength(256)]
        public string UserName { get; set; }

        public virtual ICollection<UserRole> Roles { get; set; }

        public virtual ICollection<UserClaim> UserClaims { get; set; }
        public virtual ICollection<UserLogin> UserLogins { get; set; }
        public virtual ICollection<UserToken> UserTokens { get; set; }



        public User()
        {
            Roles = new Collection<UserRole>();
            UserClaims = new Collection<UserClaim>();
            UserLogins = new Collection<UserLogin>();
            UserTokens = new Collection<UserToken>();
        }

    }
}
