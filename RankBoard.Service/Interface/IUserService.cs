using RankBoard.Dto.Identity;
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
    }
}
