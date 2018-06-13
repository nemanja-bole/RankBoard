using System;
using System.Collections.Generic;
using System.Text;

namespace RankBoard.Dto.Identity
{
    public class UserRoleDto
    {
        public string UserId { get; set; }

        public string RoleId { get; set; }

        public ApplicationUserDto User { get; set; }

        public RoleDto Role { get; set; }
    }
}
