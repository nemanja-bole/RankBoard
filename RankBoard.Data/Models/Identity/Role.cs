using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace RankBoard.Data.Models.Identity
{
    public class Role
    {
        [Key]
        [StringLength(450)]
        public string Id { get; set; }
        
        public string ConcurrencyStamp { get; set; }

        [StringLength(256)]
        public string Name { get; set; }

        [StringLength(256)]
        public string NormalizedName { get; set; }

        public virtual ICollection<UserRole> Users { get; set; }

        public virtual ICollection<RoleClaim> RoleClaims { get; set; }

        public Role()
        {
            Users = new Collection<UserRole>();

            RoleClaims = new Collection<RoleClaim>();
        }
    }
}
