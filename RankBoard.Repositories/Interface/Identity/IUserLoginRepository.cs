using RankBoard.Data.Models.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RankBoard.Repositories.Interface.Identity
{
    public interface IUserLoginRepository : IRepository<UserLogin, UserLoginKey>
    {
        IEnumerable<UserLogin> FindByUserId(string userId);
    }
}
