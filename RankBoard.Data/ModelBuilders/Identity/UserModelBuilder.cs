using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RankBoard.Data.Models.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RankBoard.Data.ModelBuilders.Identity
{
    public class UserModelBuilder : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.Roles).WithOne(x => x.User);
            builder.HasMany(x => x.UserClaims).WithOne(x => x.User);
            builder.HasMany(x => x.UserLogins).WithOne(x => x.User);
            builder.HasMany(x => x.UserTokens).WithOne(x => x.User);
        }
    }
}
