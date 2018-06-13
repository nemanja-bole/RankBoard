using System;
using System.Collections.Generic;
using System.Text;

namespace RankBoard.Dto.Identity
{
    public class UserLoginDto : UserLoginKeyDto
    {
        public string ProviderDisplayName { get; set; }
        public string UserId { get; set; }

        public ApplicationUserDto User { get; set; }
    }

    public class UserLoginKeyDto
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
    }
}
