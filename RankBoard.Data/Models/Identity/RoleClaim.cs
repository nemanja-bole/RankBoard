using System.ComponentModel.DataAnnotations;

namespace RankBoard.Data.Models.Identity
{
    public class RoleClaim : ClaimBase
    {
        [StringLength(450)]
        public string RoleId { get; set; }

        public Role Role { get; set; }
    }
}
