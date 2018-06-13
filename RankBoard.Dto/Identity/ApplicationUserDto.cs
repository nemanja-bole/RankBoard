using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace RankBoard.Dto.Identity
{
    public class ApplicationUserDto
    {
        public string Id { get; set; }
        public int AccessFailedCount { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool LockoutEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public string NormalizedEmail { get; set; }
        public string NormalizedUserName { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public string SecurityStamp { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string UserName { get; set; }

        public virtual ICollection<UserRoleDto> Roles { get; set; }

        public virtual ICollection<UserClaimDto> UserClaims { get; set; }
        public virtual ICollection<UserLoginDto> UserLogins { get; set; }
        public virtual ICollection<UserTokenDto> UserTokens { get; set; }



        public ApplicationUserDto()
        {
            Roles = new Collection<UserRoleDto>();
            UserClaims = new Collection<UserClaimDto>();
            UserLogins = new Collection<UserLoginDto>();
            UserTokens = new Collection<UserTokenDto>();
        }
    }
}
