using System;
using System.Collections.Generic;
using System.Text;

namespace RankBoard.Dto.Identity
{
    public class UserTokenDto : UserTokenKeyDto
    {
        public string Value { get; set; }
    }

    public class UserTokenKeyDto
    {
        public string UserId { get; set; }
        public string LoginProvider { get; set; }
        public string Name { get; set; }

        public ApplicationUserDto User { get; set; }
    }
}
