using RankBoard.Dto;
using System.Security.Claims;

namespace RankBoard.Service.Interface
{
    public interface IUserService
    {
        void AddRole(RoleDto roleDto);

        void RemoveRole(string id);

        void AddRoleClaim(RoleDto role, Claim claim);
    }
}
