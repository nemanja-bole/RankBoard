using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using RankBoard.Data.Models.Identity;
using RankBoard.Dto.Identity;
using RankBoard.Repositories.Interface.UnitOfWork;
using RankBoard.Service.Interface;

namespace RankBoard.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWorkIdentity _unitOfWork;
        private IMapper _mapper;


        #region RolesMethods

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
        #endregion

        #region UsersMethods

        public IQueryable<ApplicationUserDto> GetAllUsers()
        {
            return _unitOfWork.UserRepository.GetAll().Select(_mapper.Map<User, ApplicationUserDto>).AsQueryable();
        }

        public void AddUserClaims(ApplicationUserDto userDto, IEnumerable<Claim> claims)
        {
            var user = _mapper.Map<ApplicationUserDto, User>(userDto);

            var claimsEntities = claims.Select(x => new UserClaim { User = user, ClaimType = x.Type, ClaimValue = x.Value, UserId = user.Id });

            if(claimsEntities.Any())
            {
                foreach (var claim in claimsEntities)
                {
                    _unitOfWork.UserClaimRepository.Add(claim);
                }

                _unitOfWork.SaveChanges();
            }
        }

        public void AddLogin(UserLoginDto userLoginDto)
        {
            _unitOfWork.UserLoginRepository.Add(_mapper.Map<UserLoginDto, UserLogin>(userLoginDto));
            _unitOfWork.SaveChanges();
        }

        public void AddUserRole(ApplicationUserDto user, string roleName)
        {
            _unitOfWork.UserRoleRepository.Add(user.Id, roleName);
            _unitOfWork.SaveChanges();
        }

        public void AddUser(ApplicationUserDto user)
        {
            _unitOfWork.UserRepository.Add(_mapper.Map<ApplicationUserDto, User>(user));
            _unitOfWork.SaveChanges();
        }

        public void DeleteUser(ApplicationUserDto user)
        {
            _unitOfWork.UserRepository.Remove(user.Id);
            _unitOfWork.SaveChanges();
        }

        public ApplicationUserDto GetUserByNormalizedEmail(string normalizedEmail)
        {
            var userInDb = _unitOfWork.UserRepository.FindByNormalizedEmal(normalizedEmail);

            return _mapper.Map<User, ApplicationUserDto>(userInDb);
        }

        public ApplicationUserDto GetUserById(string userId)
        {
            var userInDb = _unitOfWork.UserRepository.FindById(userId);

            return _mapper.Map<User, ApplicationUserDto>(userInDb);
        }

        public ApplicationUserDto GetUserByLogin(string loginProvider, string providerKey)
        {
            var userLogin = _unitOfWork.UserLoginRepository.FindById(new UserLoginKey { LoginProvider = loginProvider, ProviderKey = providerKey });

            if(userLogin == null)
            {
                return null;
            }

            var userInDb = _unitOfWork.UserRepository.FindById(userLogin.UserId);

            return _mapper.Map<User, ApplicationUserDto>(userInDb);
        }

        public ApplicationUserDto GetUserByNormalizedUserName(string normalizedUserName)
        {
            var userInDb = _unitOfWork.UserRepository.FindNormalizedUserName(normalizedUserName);

            return _mapper.Map<User, ApplicationUserDto>(userInDb);
        }

        #endregion
    }
}
