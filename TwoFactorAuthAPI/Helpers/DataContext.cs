using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwoFactorAuth.Data.Entity;

namespace TwoFactorAuthAPI.Helpers
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetDefaultValues(modelBuilder);
            
            base.OnModelCreating(modelBuilder);
        }

        private void SetDefaultValues(ModelBuilder modelBuilder)
        {
            #region User
            modelBuilder.Entity<User>()
                        .Property(p => p.DateCreated)
                        .HasDefaultValueSql("getdate()")
                        .ValueGeneratedOnAdd();
            #endregion
        }
    }
}
