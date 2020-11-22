using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ProjectDAW.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("DBConnectionString") {

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AppDbContext,
                                    Migrations.Configuration>("DBConnectionString"));
        }
        public DbSet<TblCategory> TblCategory { get; set; }
        public DbSet<TblUser> TblUser { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}