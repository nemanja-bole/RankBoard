using System.ComponentModel.DataAnnotations;

namespace RankBoard.Data.Models.Identity
{
    public class UserClaim : ClaimBase
    {
        [StringLength(450)]
        public string UserId { get; set; }

        public User User { get; set; }
    }
}
