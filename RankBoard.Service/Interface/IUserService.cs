using RankBoard.Dto;
using System.Collections.Generic;
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
    }
}
