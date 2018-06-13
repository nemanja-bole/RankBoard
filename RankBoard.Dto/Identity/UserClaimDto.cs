using System;
using System.Collections.Generic;
using System.Text;

namespace RankBoard.Dto.Identity
{
    public class UserClaimDto
    {
        public string UserId { get; set; }

        public ApplicationUserDto User { get; set; }
    }
}
