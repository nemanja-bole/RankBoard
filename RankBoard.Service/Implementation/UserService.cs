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

        public UserService(IUnitOfWorkIdentity unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region RolesMethods

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

        public IList<Claim> GetUserClaims(ApplicationUserDto user)
        {
            return _unitOfWork.UserClaimRepository.GetByUserId(user.Id)
                .Select(x => new Claim(x.ClaimType, x.ClaimValue))
                .ToList();
        }

        public IList<UserLoginDto> GetUserLogins(ApplicationUserDto user)
        {
            return _unitOfWork.UserLoginRepository.FindByUserId(user.Id).Select(_mapper.Map<UserLogin, UserLoginDto>).ToList();
        }

        public IList<string> GetUserRoles(ApplicationUserDto user)
        {
            return _unitOfWork.UserRoleRepository.GetRolesByUserId(user.Id).Select(x => x.Name).ToList();
        }

        public UserTokenDto GetUserToken(ApplicationUserDto user, string loginProvider, string name)
        {
            var userToken = _unitOfWork.UserTokenRepository.FindById(new UserTokenKey { UserId = user.Id, LoginProvider = loginProvider, Name = name });

            return _mapper.Map<UserToken, UserTokenDto>(userToken);
        }

        public IList<ApplicationUserDto> GetUsersForClaim(Claim claim)
        {
            var users = _unitOfWork.UserClaimRepository.GetUsersForClaim(claim.Type, claim.Value).Select(_mapper.Map<User, ApplicationUserDto>);

            return users.ToList();
        }

        public IList<ApplicationUserDto> GetUsersByRoleName(string roleName)
        {
            var users = _unitOfWork.UserRoleRepository.GetUsersByRoleName(roleName).Select(_mapper.Map<User, ApplicationUserDto>);

            return users.ToList();
        }

        public IList<RoleDto> GetRoleNamesByUser(ApplicationUserDto user)
        {
            var roles = _unitOfWork.UserRoleRepository.GetRolesByUserId(user.Id).Select(_mapper.Map<Role, RoleDto>);

            return roles.ToList();
        }

        public void RemoveUserClaims(ApplicationUserDto user, IEnumerable<Claim> claims)
        {
            var claimsInDb = _unitOfWork.UserClaimRepository.GetByUserId(user.Id);

            if(claimsInDb.Any())
            {
                foreach(var claim in claims)
                {
                    var claimInDb = claimsInDb.SingleOrDefault(x => x.ClaimValue == claim.Value && x.ClaimType == claim.Type);

                    if(claimInDb != null)
                    {
                        _unitOfWork.UserClaimRepository.Remove(claimInDb.Id);
                    }
                }

                _unitOfWork.SaveChanges();
            }
        }

        public void RemoveFromRole(ApplicationUserDto user, string roleName)
        {
            _unitOfWork.UserRoleRepository.Remove(user.Id, roleName);

            _unitOfWork.SaveChanges();
        }

        public void RemoveLogin(ApplicationUserDto user, string loginProvider, string providerKey)
        {
            _unitOfWork.UserLoginRepository.Remove(new UserLoginKey { LoginProvider = loginProvider, ProviderKey = providerKey });

            _unitOfWork.SaveChanges();
        }

        public void RemoveToken(ApplicationUserDto user, string loginProvider, string name)
        {
            _unitOfWork.UserTokenRepository.Remove(new UserTokenKey { UserId = user.Id, LoginProvider = loginProvider, Name = name });

            _unitOfWork.SaveChanges();
        }

        public void ReplaceUserClaim(ApplicationUserDto user, Claim claim, Claim newClaim)
        {
            var claimInDb = _unitOfWork.UserClaimRepository.GetByUserId(user.Id)
                .SingleOrDefault(x => x.ClaimValue == claim.Value && x.ClaimType == claim.Type);

            if(claimInDb != null)
            {
                claimInDb.ClaimType = newClaim.Type;
                claimInDb.ClaimValue = newClaim.Value;

                _unitOfWork.UserClaimRepository.Update(claimInDb);
                _unitOfWork.SaveChanges();
            }
        }

        public void AddToken(ApplicationUserDto user, string loginProvider, string name, string value)
        {
            var userTokenEntity = new UserToken
            {
                LoginProvider = loginProvider,
                Name = name,
                Value = value,
                UserId = user.Id                
            };

            _unitOfWork.UserTokenRepository.Add(userTokenEntity);
            _unitOfWork.SaveChanges();
        }

        public void UpdateUser(ApplicationUserDto user)
        {
            _unitOfWork.UserRepository.Update(_mapper.Map<ApplicationUserDto, User>(user));
            _unitOfWork.SaveChanges();
        }

        #endregion
    }
}
