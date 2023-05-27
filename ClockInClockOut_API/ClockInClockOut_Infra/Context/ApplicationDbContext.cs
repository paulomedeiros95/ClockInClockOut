using ClockInClockOut_Domain.Domains;
using ClockInClockOut_Infra.Builders;
using ClockInClockOut_Infra.Seed;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ClockInClockOut_Infra.Context
{
    public class ApplicationDbContext : DbContext
    {
        #region DbSet

        public virtual DbSet<UserDomain> Users { get; set; }

        public virtual DbSet<ProjectDomain> Project { get; set; }

        public virtual DbSet<TimeDomain> Times { get; set; }

        #endregion

        #region Constructor

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
        {
            Database.Migrate();
        }
        #endregion

        #region Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDomain>(new UserConfiguration().Configure);
            modelBuilder.Entity<ProjectDomain>(new ProjectConfiguration().Configure);
            modelBuilder.Entity<TimeDomain>(new TimeConfiguration().Configure);

            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }
        #endregion
    }
}
