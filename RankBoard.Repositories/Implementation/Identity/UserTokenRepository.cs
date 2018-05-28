using Microsoft.EntityFrameworkCore;
using RankBoard.Data.Models.Identity;
using RankBoard.Repositories.Interface;

namespace RankBoard.Repositories.Implementation.Identity
{
    public class UserTokenRepository : Repository<UserToken, UserTokenKey>
    {
        public UserTokenRepository(DbContext context) : base(context)
        {
        }
    }
}
