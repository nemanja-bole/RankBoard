using Microsoft.EntityFrameworkCore;
using RankBoard.Data.Models.Identity;
using RankBoard.Repositories.Implementation.Identity;
using RankBoard.Repositories.Interface;
using RankBoard.Repositories.Interface.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RankBoard.Repositories
{
    public class UnitOfWorkIdentity : IUnitOfWork, IDisposable
    {
        private readonly DbContext _context;

        private IRoleRepository _roleRepository;
        private IRoleClaimRepository _roleClaimRepository;
        private IUserRepository _userRepository;
        private IUserClaimRepository _userClaimRepository;
        private IUserLoginRepository _userLoginRepository;
        private IRepository<UserToken, UserTokenKey> _userTokenRepository;
        private IUserRoleRepository _userRoleRepository;
        public UnitOfWorkIdentity(DbContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository
        {
            get { return _userRepository ?? (_userRepository = new UserRepository(_context)); }
        }
        
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancelationToken)
        {
            return _context.SaveChangesAsync(cancelationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
