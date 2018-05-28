using RankBoard.Data.Models.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RankBoard.Repositories.Interface.Identity
{
    public interface IUserRoleRepository
    {
        void Add(string userId, string roleName);
        void Remove(string userId, string roleName);
        IEnumerable<Role> GetRolesByUserId(string userId);
        IEnumerable<User> GetUsersByRoleName(string roleName);
    }
}
