using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CentralizedAuth.Api.Models
{
    public class RuleAppDbContext : DbContext
    {
        public RuleAppDbContext(DbContextOptions<RuleAppDbContext> options) : base(options) { }

        public DbSet<RuleApplication> RuleApp { get; set; }
    }
}
