using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RankBoard.Data.Models.Identity
{
    public class Role
    {
        public string Id { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string Name { get; set; }
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
