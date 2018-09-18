using System.ComponentModel.DataAnnotations;

namespace RankBoard.Data.Models.Identity
{
    public class UserToken : UserTokenKey
    {
        public string Value { get; set; }
    }

    public class UserTokenKey
    {
        [StringLength(450)]
        public string UserId { get; set; }

        [StringLength(128)]
        public string LoginProvider { get; set; }

        [StringLength(128)]
        public string Name { get; set; }

        public User User { get; set; }
    }
}
