using System;
using System.Collections.Generic;
using System.Text;

namespace RankBoard.Dto
{
    public class RoleClaimDto : ClaimBaseDto
    {
        public string RoleId { get; set; }

        public RoleDto RoleDto { get; set; }
    }
}
