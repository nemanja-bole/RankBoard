using RankBoard.Data.Contexts;
using RankBoard.Data.Models.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RankBoard.Repositories.Interface.Identity
{
    public interface IUserRepository : IRepository<User, string> 
    {
        User FindNormalizedUserName(string normalizedUserName);

        User FindByNormalizedEmal(string normalizedEmail);

        IEnumerable<User> FindUsersByRoleName(string roleName);
    }
}
