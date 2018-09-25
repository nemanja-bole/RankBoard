using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RankBoard.Data.Models.Identity
{
    [Table("UserRoles")]
    public class UserRole
    {
        [StringLength(450)]
        public string UserId { get; set; }

        [StringLength(450)]
        public string RoleId { get; set; }

        public User User { get; set; }

        public Role Role { get; set; }
    }
}
