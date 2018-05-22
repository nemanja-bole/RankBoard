using RankBoard.Data.Models.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RankBoard.Repositories.Interface.Identity
{
    public interface IRoleClaimRepository : IRepository<RoleClaim, int>
    {
        IEnumerable<RoleClaim> FindByRoleId(string roleId);
    }
}
