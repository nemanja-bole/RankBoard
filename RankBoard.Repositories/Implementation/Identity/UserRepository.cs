using Microsoft.EntityFrameworkCore;
using RankBoard.Data.Models.Identity;
using RankBoard.Repositories.Interface.Identity;
using System.Linq;

namespace RankBoard.Repositories.Implementation.Identity
{
    public class UserRepository : Repository<User, string>, IUserRepository
    {
        public UserRepository(DbContext context):base(context)
        { }        

        public User FindByNormalizedEmal(string normalizedEmail)
        {
            return Set.FirstOrDefault(x => x.NormalizedEmail == normalizedEmail);
        }

        public User FindNormalizedUserName(string normalizedUserName)
        {
            return Set.FirstOrDefault(x => x.NormalizedUserName == normalizedUserName);
        }
    }
}
