using Microsoft.AspNetCore.Identity;
using RankBoard.Dto;
using RankBoard.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace RankBoard.Ids.Identity
{
    public class CustomRoleStore : IRoleStore<IdentityRole>, IRoleClaimStore<IdentityRole>
    {
        private readonly IUserService _userSerivice;

        public CustomRoleStore(IUserService userService)
        {
            _userSerivice = userService;
        }

        public Task AddClaimAsync(IdentityRole role, Claim claim, CancellationToken cancellationToken = default(CancellationToken))
        {           
            if (cancellationToken != null)
            {
                cancellationToken.ThrowIfCancellationRequested();
            }

            if (role == null)
            {
                throw new ArgumentNullException();
            }

            if (claim == null)
            {
                throw new ArgumentNullException();
            }

            _userSerivice.AddRoleClaim(getRoleEntity(role), claim);

            return Task.CompletedTask;            
        }

        public Task<IdentityResult> CreateAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            try
            {
                if(cancellationToken != null)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                }

                if (role == null)
                {
                    throw new ArgumentNullException(nameof(role));
                }

                var roleEntity = getRoleEntity(role);

                _userSerivice.AddRole(roleEntity);

                return Task.FromResult(IdentityResult.Success);
            }
            catch (Exception ex)
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Code = ex.Message, Description = ex.Message }));
            }
        }

        public Task<IdentityResult> DeleteAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            try
            {
                if (cancellationToken != null)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                }

                if (role == null)
                {
                    throw new ArgumentNullException(nameof(role));
                }

                _userSerivice.RemoveRole(role.Id);

                return Task.FromResult(IdentityResult.Success);
            }
            catch (Exception ex)
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Code = ex.Message, Description = ex.Message }));
            }
        }

        public void Dispose()
        {
            // Lifetimes of dependencies are managed by the IoC container, so disposal here is unnecessary.
        }

        public Task<IdentityRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            if(cancellationToken != null)
            {
                cancellationToken.ThrowIfCancellationRequested();
            }

            if(string.IsNullOrWhiteSpace(roleId))
            {
                throw new ArgumentNullException(nameof(roleId));
            }

            if(!Guid.TryParse(roleId, out Guid id))
            {
                throw new ArgumentOutOfRangeException(nameof(roleId), $"{nameof(roleId)} is not valid GUID");
            }

            var roleEntity = _userSerivice.FindRoleById(id.ToString());

            return Task.FromResult(getIdentityRole(roleEntity));
        }

        public Task<IdentityRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            if (cancellationToken != null)
            {
                cancellationToken.ThrowIfCancellationRequested();
            }

            if (string.IsNullOrWhiteSpace(normalizedRoleName))
            {
                throw new ArgumentNullException(nameof(normalizedRoleName));
            }

            var roleEntity = _userSerivice.FindRoleByName(normalizedRoleName);

            return Task.FromResult(getIdentityRole(roleEntity));
        }

        public Task<IList<Claim>> GetClaimsAsync(IdentityRole role, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (cancellationToken != null)
            {
                cancellationToken.ThrowIfCancellationRequested();
            }

            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            var result = _userSerivice.GetRoleClaims(role.Id);

            return Task.FromResult(result);
        }

        public Task<string> GetNormalizedRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            if (cancellationToken != null)
                cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            return Task.FromResult(role.NormalizedName);
        }

        public Task<string> GetRoleIdAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            if (cancellationToken != null)
                cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            return Task.FromResult(role.Id);
        }

        public Task<string> GetRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            if (cancellationToken != null)
                cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            return Task.FromResult(role.Name);
        }

        public Task RemoveClaimAsync(IdentityRole role, Claim claim, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (cancellationToken != null)
                cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            if (claim == null)
                throw new ArgumentNullException(nameof(claim));

            _userSerivice.RemoveClaim(getRoleEntity(role), claim);

            return Task.CompletedTask;
        }

        public Task SetNormalizedRoleNameAsync(IdentityRole role, string normalizedName, CancellationToken cancellationToken)
        {
            if (cancellationToken != null)
                cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            role.NormalizedName = normalizedName;

            return Task.CompletedTask;
        }

        public Task SetRoleNameAsync(IdentityRole role, string roleName, CancellationToken cancellationToken)
        {
            if (cancellationToken != null)
                cancellationToken.ThrowIfCancellationRequested();

            if (role == null)
                throw new ArgumentNullException(nameof(role));

            role.Name = roleName;

            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            try
            {
                if (cancellationToken != null)
                    cancellationToken.ThrowIfCancellationRequested();

                if (role == null)
                    throw new ArgumentNullException(nameof(role));

                var roleEntity = getRoleEntity(role);

                _userSerivice.UpdateRole(roleEntity);

                return Task.FromResult(IdentityResult.Success);

            }
            catch (Exception ex)
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Code = ex.Message, Description = ex.Message } ));
            }
        }


        private RoleDto getRoleEntity(IdentityRole value)
        {
            return value == null
                ? default(RoleDto)
                : new RoleDto
                {
                    ConcurrencyStamp = value.ConcurrencyStamp,
                    Id = value.Id,
                    Name = value.Name,
                    NormalizedName = value.NormalizedName
                };
        }

        private IdentityRole getIdentityRole(RoleDto value)
        {
            return value == null
                ? default(IdentityRole)
                : new IdentityRole
                {
                    ConcurrencyStamp = value.ConcurrencyStamp,
                    Id = value.Id,
                    Name = value.Name,
                    NormalizedName = value.NormalizedName
                };
        }
    }
}
