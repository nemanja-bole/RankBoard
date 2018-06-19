﻿using RankBoard.Dto.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace RankBoard.Service.Interface
{
    public interface IUserService
    {
        void AddRole(RoleDto roleDto);

        void RemoveRole(string id);

        void AddRoleClaim(RoleDto roleDto, Claim claim);

        RoleDto FindRoleById(string id);

        RoleDto FindRoleByName(string name);

        IList<Claim> GetRoleClaims(string roleId);

        void UpdateRole(RoleDto role);

        void RemoveClaim(RoleDto roleDto, Claim claim);

        IQueryable<ApplicationUserDto> GetAllUsers();

        void AddUserClaims(ApplicationUserDto userDto, IEnumerable<Claim> claims);

        void AddLogin(UserLoginDto userLoginDto);

        void AddUserRole(ApplicationUserDto user, string roleName);

        void AddUser(ApplicationUserDto user);

        void DeleteUser(ApplicationUserDto user);

        ApplicationUserDto GetUserByNormalizedEmail(string normalizedEmail);

        ApplicationUserDto GetUserById(string userId);

        ApplicationUserDto GetUserByLogin(string loginProvider, string providerKey);

        ApplicationUserDto GetUserByNormalizedUserName(string normalizedUserName);

        IList<Claim> GetUserClaims(ApplicationUserDto user);

        IList<UserLoginDto> GetUserLogins(ApplicationUserDto user);

        IList<string> GetUserRoles(ApplicationUserDto user);

        UserTokenDto GetUserToken(ApplicationUserDto user, string loginProvider, string name);

        IList<ApplicationUserDto> GetUsersForClaim(Claim claim);

        IList<ApplicationUserDto> GetUsersByRoleName(string roleName);

        IList<RoleDto> GetRoleNamesByUser(ApplicationUserDto user);

        void RemoveUserClaims(ApplicationUserDto user, IEnumerable<Claim> claims);

        void RemoveFromRole(ApplicationUserDto user, string roleName);

        void RemoveLogin(ApplicationUserDto user, string loginProvider, string providerKey);

        void RemoveToken(ApplicationUserDto user, string loginProvider, string name);

        void ReplaceUserClaim(ApplicationUserDto user, Claim claim, Claim newClaim);

        void UpdateUser(ApplicationUserDto user);

        void AddToken(ApplicationUserDto user, string loginProvider, string name, string value);
    }
}
