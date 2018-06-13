using System;
using System.Collections.Generic;
using System.Text;

namespace RankBoard.Dto.Identity
{
    public class RoleDto
    {
        public string Id { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }
    }
}
