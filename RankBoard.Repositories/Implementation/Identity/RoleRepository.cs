using Microsoft.EntityFrameworkCore;
using RankBoard.Data.Models.Identity;
using RankBoard.Repositories.Interface;
using System.Linq;

namespace RankBoard.Repositories.Implementation.Identity
{
    public class RoleRepository : Repository<Role, string>, IRoleRepository
    {
        public RoleRepository(DbContext context) : base(context)
        { }

        public Role FindByName(string roleName)
        {
            return Set.FirstOrDefault(x => x.Name == roleName);
        }
    }
}
