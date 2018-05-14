using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RankBoard.Data.Contexts
{
    public class RankBoardDbContext : DbContext
    {
        public RankBoardDbContext(DbContextOptions<RankBoardDbContext> options) : base(options)
        {                
        }
    }
}
