using System;
using System.Collections.Generic;
using System.Text;

namespace RankBoard.Dto
{
    public class ClaimBaseDto
    {
        public int Id { get; set; }
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
    }
}
