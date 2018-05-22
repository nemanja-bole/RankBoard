namespace RankBoard.Data.Models.Identity
{
    public class UserClaim : ClaimBase
    {
        public string UserId { get; set; }

        public User User { get; set; }
    }
}
