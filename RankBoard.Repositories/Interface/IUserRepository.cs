using RankBoard.Data.Models.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RankBoard.Repositories.Interface
{
    public interface IUserRepository : IRepository<User, string>
    {
        User FindNormalizedUserName(string normalizedUserName);

        User FindByNormalizedEmal(string normalizedEmail);
    }
}
