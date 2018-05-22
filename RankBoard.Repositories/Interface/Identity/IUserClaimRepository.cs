using RankBoard.Data.Models.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RankBoard.Repositories.Interface.Identity
{
    public interface IUserClaimRepository : IRepository<UserClaim, int>
    {
        IEnumerable<UserClaim> GetByUserId(string userId);
        IEnumerable<User> GetUsersForClaim(string claimType, string claimValue);
    }
}
