using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RankBoard.Data.Models.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RankBoard.Data.ModelBuilders.Identity
{
    public class UserRoleModelBuilder : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(x => new { x.UserId, x.RoleId});
        }
    }
}
