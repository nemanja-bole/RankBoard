using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using RankBoard.Data.Models.Identity;
using RankBoard.Dto;
using RankBoard.Repositories.Interface.UnitOfWork;
using RankBoard.Service.Interface;

namespace RankBoard.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWorkIdentity _unitOfWork;
        private IMapper _mapper;

        public UserService(IUnitOfWorkIdentity unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void AddRole(RoleDto roleDto)
        {
            _unitOfWork.RoleRepository.Add(_mapper.Map<RoleDto, Role>(roleDto));
            _unitOfWork.SaveChanges();
        }

        public void AddRoleClaim(RoleDto roleDto, Claim claim)
        {
            var roleInDb = _unitOfWork.RoleRepository.FindById(roleDto.Id);

            var roleClaim = new RoleClaim()
            {
                ClaimType = claim.Type,
                ClaimValue = claim.Value,
                Role = roleInDb,
                RoleId = roleInDb.Id
            };

            _unitOfWork.RoleClaimRepository.Add(roleClaim);
        }

        public RoleDto FindRoleById(string id)
        {
            var role = _unitOfWork.RoleRepository.FindById(id);

            return _mapper.Map<Role, RoleDto>(role);
        }

        public RoleDto FindRoleByName(string name)
        {
            var role = _unitOfWork.RoleRepository.FindByName(name);

            return _mapper.Map<Role, RoleDto>(role);
        }

        public IList<Claim> GetRoleClaims(string roleId)
        {
            var claims = _unitOfWork.RoleClaimRepository.FindByRoleId(roleId).Select(x => new Claim(x.ClaimType, x.ClaimValue));

            return claims.ToList();
        }

        public void RemoveClaim(RoleDto roleDto, Claim claim)
        {
            var roleClaimInDb = _unitOfWork.RoleClaimRepository
                .FindByRoleId(roleDto.Id)
                .SingleOrDefault(x => x.ClaimType == claim.Type && x.ClaimValue == claim.Value);

            if(roleClaimInDb != null)
            {
                _unitOfWork.RoleClaimRepository.Remove(roleClaimInDb);
                _unitOfWork.SaveChanges();
            }
        }

        public void RemoveRole(string id)
        {
            var roleToRemove = _unitOfWork.RoleRepository.FindById(id);

            _unitOfWork.RoleRepository.Remove(roleToRemove);
            _unitOfWork.SaveChanges();
        }

        public void UpdateRole(RoleDto role)
        {
            _unitOfWork.RoleRepository.Update(_mapper.Map<RoleDto, Role>(role));
            _unitOfWork.SaveChanges();
        }
    }
}
