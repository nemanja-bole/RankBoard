using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using RankBoard.Dto.Identity;
using RankBoard.Service.Interface;

namespace RankBoard.Ids.Identity
{
    public class CustomUserStore :
        IUserStore<ApplicationUserDto>,
        IUserPasswordStore<ApplicationUserDto>,
        IUserEmailStore<ApplicationUserDto>,
        IUserLoginStore<ApplicationUserDto>,
        IUserRoleStore<ApplicationUserDto>,
        IUserSecurityStampStore<ApplicationUserDto>,
        IUserClaimStore<ApplicationUserDto>,
        IUserAuthenticationTokenStore<ApplicationUserDto>,
        IUserTwoFactorStore<ApplicationUserDto>,
        IUserPhoneNumberStore<ApplicationUserDto>,
        IUserLockoutStore<ApplicationUserDto>,
        IQueryableUserStore<ApplicationUserDto>

    {
        private readonly IUserService _userService;

        public CustomUserStore(IUserService userService)
        {
            _userService = userService;
        }

        public IQueryable<ApplicationUserDto> Users
        {
            get
            {
                return _userService.GetAllUsers();
            }
        }

        public Task AddClaimsAsync(ApplicationUserDto user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            if(cancellationToken != null)
            {
                cancellationToken.ThrowIfCancellationRequested();
            }

            if(user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if(claims == null)
            {
                throw new ArgumentNullException(nameof(claims));
            }

            _userService.AddUserClaims(user, claims);

            return Task.CompletedTask;
        }

        public Task AddLoginAsync(ApplicationUserDto user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            if (cancellationToken != null)
            {
                cancellationToken.ThrowIfCancellationRequested();
            }

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (login == null)
            {
                throw new ArgumentNullException(nameof(login));
            }

            if(string.IsNullOrWhiteSpace(login.LoginProvider))
            {
                throw new ArgumentNullException(nameof(login.LoginProvider));
            }

            if(string.IsNullOrWhiteSpace(login.ProviderKey))
            {
                throw new ArgumentNullException(nameof(login.ProviderKey));
            }

            var loginDto = new UserLoginDto
            {
                LoginProvider = login.LoginProvider,
                ProviderDisplayName = login.ProviderDisplayName,
                ProviderKey = login.ProviderKey,
                UserId = user.Id
            };

            _userService.AddLogin(loginDto);

            return Task.CompletedTask;

        }

        public Task AddToRoleAsync(ApplicationUserDto user, string roleName, CancellationToken cancellationToken)
        {
            if (cancellationToken != null)
                cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (string.IsNullOrWhiteSpace(roleName))
                throw new ArgumentNullException(nameof(roleName));

            _userService.AddUserRole(user, roleName);

            return Task.CompletedTask;
        }

        public Task<IdentityResult> CreateAsync(ApplicationUserDto user, CancellationToken cancellationToken)
        {
            try
            {
                if (cancellationToken != null)
                    cancellationToken.ThrowIfCancellationRequested();

                if (user == null)
                    throw new ArgumentNullException(nameof(user));

                _userService.AddUser(user);

                return Task.FromResult(IdentityResult.Success);
            }
            catch (Exception ex)
            {
                return Task.FromResult(IdentityResult.Failed(new IdentityError { Code = ex.Message, Description = ex.Message }));
            }
        }

        public Task<IdentityResult> DeleteAsync(ApplicationUserDto user, CancellationToken cancellationToken)
        {
            try
            {
                if (cancellationToken != null)
                    cancellationToken.ThrowIfCancellationRequested();

                if (user == null)
                    throw new ArgumentNullException(nameof(user));

                _userService.DeleteUser(user);

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

        public Task<ApplicationUserDto> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            if (cancellationToken != null)
            {
                cancellationToken.ThrowIfCancellationRequested();
            }

            if(string.IsNullOrWhiteSpace(normalizedEmail))
            {
                throw new ArgumentNullException(nameof(normalizedEmail));
            }

            return Task.FromResult(_userService.GetUserByNormalizedEmail(normalizedEmail));
        }

        public Task<ApplicationUserDto> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            if (cancellationToken != null)
            {
                cancellationToken.ThrowIfCancellationRequested();
            }

            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return Task.FromResult(_userService.GetUserById(userId));
        }

        public Task<ApplicationUserDto> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            if (cancellationToken != null)
                cancellationToken.ThrowIfCancellationRequested();

            if (string.IsNullOrWhiteSpace(loginProvider))
                throw new ArgumentNullException(nameof(loginProvider));

            if (string.IsNullOrWhiteSpace(providerKey))
                throw new ArgumentNullException(nameof(providerKey));

            return Task.FromResult(_userService.GetUserByLogin(loginProvider, providerKey));
        }

        public Task<ApplicationUserDto> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            if (cancellationToken != null)
                cancellationToken.ThrowIfCancellationRequested();

            var user = _userService.GetUserByNormalizedUserName(normalizedUserName);

            return Task.FromResult(user);
        }

        public Task<int> GetAccessFailedCountAsync(ApplicationUserDto user, CancellationToken cancellationToken)
        {
            if (cancellationToken != null)
                cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.AccessFailedCount);
        }

        public Task<IList<Claim>> GetClaimsAsync(ApplicationUserDto user, CancellationToken cancellationToken)
        {
            if (cancellationToken != null)
                cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            IList<Claim> result = _userService.GetUserClaims(user);

            return Task.FromResult(result);
        }

        public Task<string> GetEmailAsync(ApplicationUserDto user, CancellationToken cancellationToken)
        {
            if (cancellationToken != null)
                cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(ApplicationUserDto user, CancellationToken cancellationToken)
        {
            if (cancellationToken != null)
                cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.EmailConfirmed);
        }

        public Task<bool> GetLockoutEnabledAsync(ApplicationUserDto user, CancellationToken cancellationToken)
        {
            if (cancellationToken != null)
                cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.LockoutEnabled);
        }

        public Task<DateTimeOffset?> GetLockoutEndDateAsync(ApplicationUserDto user, CancellationToken cancellationToken)
        {
            if (cancellationToken != null)
                cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.LockoutEnd);
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(ApplicationUserDto user, CancellationToken cancellationToken)
        {
            if (cancellationToken != null)
                cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            IList<UserLoginInfo> result = _userService.GetUserLogins(user)
                .Select(x => new UserLoginInfo(x.LoginProvider, x.ProviderKey, x.ProviderDisplayName)).ToList();

            return Task.FromResult(result);
        }

        public Task<string> GetNormalizedEmailAsync(ApplicationUserDto user, CancellationToken cancellationToken)
        {
            if (cancellationToken != null)
                cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.NormalizedEmail);
        }

        public Task<string> GetNormalizedUserNameAsync(ApplicationUserDto user, CancellationToken cancellationToken)
        {
            if (cancellationToken != null)
                cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.NormalizedUserName);
        }

        public Task<string> GetPasswordHashAsync(ApplicationUserDto user, CancellationToken cancellationToken)
        {
            if (cancellationToken != null)
                cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.PasswordHash);
        }

        public Task<string> GetPhoneNumberAsync(ApplicationUserDto user, CancellationToken cancellationToken)
        {
            if (cancellationToken != null)
                cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(ApplicationUserDto user, CancellationToken cancellationToken)
        {
            if (cancellationToken != null)
                cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public Task<IList<string>> GetRolesAsync(ApplicationUserDto user, CancellationToken cancellationToken)
        {
            if (cancellationToken != null)
                cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            IList<string> result = _userService.GetUserRoles(user);

            return Task.FromResult(result);
        }

        public Task<string> GetSecurityStampAsync(ApplicationUserDto user, CancellationToken cancellationToken)
        {
            if (cancellationToken != null)
                cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.SecurityStamp);
        }

        public Task<string> GetTokenAsync(ApplicationUserDto user, string loginProvider, string name, CancellationToken cancellationToken)
        {
            if (cancellationToken != null)
                cancellationToken.ThrowIfCancellationRequested();

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (string.IsNullOrWhiteSpace(loginProvider))
                throw new ArgumentNullException(nameof(loginProvider));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            var userToken = _userService.GetUserToken(user, loginProvider, name);

            return Task.FromResult(userToken?.Name);
        }

        public Task<bool> GetTwoFactorEnabledAsync(ApplicationUserDto user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserIdAsync(ApplicationUserDto user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserNameAsync(ApplicationUserDto user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<ApplicationUserDto>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<ApplicationUserDto>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(ApplicationUserDto user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<int> IncrementAccessFailedCountAsync(ApplicationUserDto user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsInRoleAsync(ApplicationUserDto user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveClaimsAsync(ApplicationUserDto user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(ApplicationUserDto user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveLoginAsync(ApplicationUserDto user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveTokenAsync(ApplicationUserDto user, string loginProvider, string name, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task ReplaceClaimAsync(ApplicationUserDto user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task ResetAccessFailedCountAsync(ApplicationUserDto user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailAsync(ApplicationUserDto user, string email, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(ApplicationUserDto user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetLockoutEnabledAsync(ApplicationUserDto user, bool enabled, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetLockoutEndDateAsync(ApplicationUserDto user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedEmailAsync(ApplicationUserDto user, string normalizedEmail, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(ApplicationUserDto user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(ApplicationUserDto user, string passwordHash, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetPhoneNumberAsync(ApplicationUserDto user, string phoneNumber, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetPhoneNumberConfirmedAsync(ApplicationUserDto user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetSecurityStampAsync(ApplicationUserDto user, string stamp, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetTokenAsync(ApplicationUserDto user, string loginProvider, string name, string value, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetTwoFactorEnabledAsync(ApplicationUserDto user, bool enabled, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetUserNameAsync(ApplicationUserDto user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(ApplicationUserDto user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
