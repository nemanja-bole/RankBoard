using Microsoft.EntityFrameworkCore;
using RankBoard.Data.Models.Identity;
using RankBoard.Repositories.Interface.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RankBoard.Repositories.Implementation.Identity
{
    public class UserLoginRepository : Repository<UserLogin, UserLoginKey>, IUserLoginRepository
    {
        public UserLoginRepository(DbContext context) : base(context)
        {
        }

        public IEnumerable<UserLogin> FindByUserId(string userId)
        {
            return Set.Where(x => x.UserId == userId);
        }
    }
}
