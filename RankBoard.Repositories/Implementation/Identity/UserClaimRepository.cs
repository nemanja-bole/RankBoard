using Microsoft.EntityFrameworkCore;
using RankBoard.Data.Models.Identity;
using RankBoard.Repositories.Interface.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RankBoard.Repositories.Implementation.Identity
{
    public class UserClaimRepository : Repository<UserClaim, int>, IUserClaimRepository
    {
        public UserClaimRepository(DbContext context) : base(context)
        { }

        public IEnumerable<UserClaim> GetByUserId(string userId)
        {
            return Set.Where(x => x.UserId == userId);
        }

        public IEnumerable<User> GetUsersForClaim(string claimType, string claimValue)
        {
            return Set.Where(x => x.ClaimType == claimType && x.ClaimValue == claimValue).Select(x => x.User);
        }
    }
}
