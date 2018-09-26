using Microsoft.AspNetCore.Identity;
using RankBoard.Dto.Identity;

namespace RankBoard.Ids.Identity
{
    public class IdentityDataInitializer
    {
        public static void SeedData(UserManager<ApplicationUserDto> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedUsers(UserManager<ApplicationUserDto> userManager)
        {
            if(userManager.FindByEmailAsync("djuramutavi894@gmail.com").Result == null)
            {
                ApplicationUserDto user = new ApplicationUserDto();
                user.UserName = "djuramutavi894@gmail.com";
                user.NormalizedUserName = "DJURAMUTAVI894@GMAIL.COM";
                user.Email = "djuramutavi894@gmail.com";
                user.NormalizedEmail = "DJURAMUTAVI894@GMAIL.COM";
                user.LockoutEnabled = false;
                user.EmailConfirmed = true;

                var result = userManager.CreateAsync(user, "Password11").Result;

                if(result.Succeeded)
                {
                    var userDb = userManager.FindByEmailAsync(user.Email).Result;

                    userManager.SetLockoutEnabledAsync(userDb, false).Wait();
                    userManager.AddToRoleAsync(userDb, "Admin").Wait();
                }
            }
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if(!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = "ADMIN";                

                var result = roleManager.CreateAsync(role).Result;
            }
            
            if (!roleManager.RoleExistsAsync("Player").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Player";
                role.NormalizedName = "PLAYER";

                var result = roleManager.CreateAsync(role).Result;
            }
        }
    }
}
