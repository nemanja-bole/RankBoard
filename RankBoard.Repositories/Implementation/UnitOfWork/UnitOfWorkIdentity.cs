using Microsoft.EntityFrameworkCore;
using RankBoard.Data.Models.Identity;
using RankBoard.Repositories.Implementation.Identity;
using RankBoard.Repositories.Interface;
using RankBoard.Repositories.Interface.Identity;
using RankBoard.Repositories.Interface.UnitOfWork;
using System;

namespace RankBoard.Repositories.Implementation.UnitOfWork
{
    public class UnitOfWorkIdentity : BaseUnitOfWork, IUnitOfWorkIdentity, IDisposable
    {
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

        public IRoleRepository RoleRepository
        {
            get { return _roleRepository ?? (_roleRepository = new RoleRepository(_context)); }
        }

        public IRoleClaimRepository RoleClaimRepository
        {
            get { return _roleClaimRepository ?? (_roleClaimRepository = new RoleClaimRepository(_context)); }
        }

        public IUserRepository UserRepository
        {
            get { return _userRepository ?? (_userRepository = new UserRepository(_context)); }
        }

        public IUserClaimRepository UserClaimRepository
        {
            get { return _userClaimRepository ?? (_userClaimRepository = new UserClaimRepository(_context)); } 
        }

        public IUserLoginRepository UserLoginRepository
        {
            get { return _userLoginRepository ?? (_userLoginRepository = new UserLoginRepository(_context)); } 
        }

        public IRepository<UserToken, UserTokenKey> UserTokenRepository
        {
            get { return _userTokenRepository ?? (_userTokenRepository = new UserTokenRepository(_context)); } 
        }

        public IUserRoleRepository UserRoleRepository
        {
            get { return _userRoleRepository ?? (_userRoleRepository = new UserRoleRepository(_context)); }
        }
    }
}
