using System.ComponentModel.DataAnnotations;

namespace RankBoard.Data.Models.Identity
{
    public class UserLogin : UserLoginKey
    {
        public string ProviderDisplayName { get; set; }

        [StringLength(450)]
        public string UserId { get; set; }

        public User User { get; set; }
    }

    public class UserLoginKey
    {
        [StringLength(128)]
        public string LoginProvider { get; set; }

        [StringLength(128)]
        public string ProviderKey { get; set; }
    }
}
