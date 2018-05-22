using Microsoft.EntityFrameworkCore;
using RankBoard.Data.Models.Identity;
using RankBoard.Repositories.Interface.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RankBoard.Repositories.Implementation.Identity
{
    public class RoleClaimRepository : Repository<RoleClaim, int>, IRoleClaimRepository
    {
        public RoleClaimRepository(DbContext context) : base(context)
        { }

        public IEnumerable<RoleClaim> FindByRoleId(string roleId)
        {
            return Set.Where(x => x.RoleId == roleId);
        }
    }
}
