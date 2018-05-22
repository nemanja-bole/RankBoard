using RankBoard.Data.Models.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RankBoard.Repositories.Interface
{
    public interface IRoleRepository : IRepository<Role, string>
    {
        Role FindByName(string roleName);
    }
}
