namespace RankBoard.Data.Models.Identity
{
    public class RoleClaim : ClaimBase
    {
        public string RoleId { get; set; }

        public Role Role { get; set; }
    }
}
