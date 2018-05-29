﻿using RankBoard.Data.Models.Identity;
using RankBoard.Repositories.Interface;
using RankBoard.Repositories.Interface.Identity;

namespace RankBoard.Repositories
{
    public interface IUnitOfWorkIdentity
    {
        IRoleRepository RoleRepository { get; }
        IRoleClaimRepository RoleClaimRepository { get; }
        IUserRepository UserRepository { get; }
        IUserClaimRepository UserClaimRepository { get; }
        IUserLoginRepository UserLoginRepository { get; }
        IRepository<UserToken, UserTokenKey> UserTokenRepository { get; }
        IUserRoleRepository UserRoleRepository { get; }
    }
}
