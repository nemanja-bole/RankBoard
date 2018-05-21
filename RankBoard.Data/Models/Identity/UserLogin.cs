namespace RankBoard.Data.Models.Identity
{
    public class UserLogin : UserLoginKey
    {
        public string ProviderDisplayName { get; set; }
        public string UserId { get; set; }
    }

    public class UserLoginKey
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
    }
}
