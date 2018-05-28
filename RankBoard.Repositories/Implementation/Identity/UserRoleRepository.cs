using Microsoft.EntityFrameworkCore;
using RankBoard.Data.Contexts;
using RankBoard.Data.Models.Identity;
using RankBoard.Repositories.Interface.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RankBoard.Repositories.Implementation.Identity
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private DbContext _context;
        private DbSet<UserRole> _set;

        public UserRoleRepository(DbContext context)
        {
            _context = context;
        }

        protected DbSet<UserRole> Set
        {
            get { return _set ?? (_set = _context.Set<UserRole>()); }
        }

        protected ApplicationDbContext Context
        {
            get { return _context as ApplicationDbContext; }
        }


        public void Add(string userId, string roleName)
        {
            var user = Context.Users.FirstOrDefault(x => x.Id == userId);
            var role = Context.Roles.FirstOrDefault(x => x.Name == roleName);

            Set.Add(new UserRole { Role = role, User = user });
        }

        public IEnumerable<Role> GetRolesByUserId(string userId)
        {
            return Set.Where(x => x.UserId == userId).Select(x => x.Role);
        }

        public IEnumerable<User> GetUsersByRoleName(string roleName)
        {
            return Set.Where(x => x.Role.Name == roleName).Select(x => x.User);
        }

        public void Remove(string userId, string roleName)
        {
            var userRole = Set.FirstOrDefault(x => x.UserId == userId && x.Role.Name == roleName);

            Set.Remove(userRole);            
        }
    }
}
