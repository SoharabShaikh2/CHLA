using EQLWebAPIWithAngular.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EQLWebAPIWithAngular.DatabaseContext
{
    public class MySqlDbContext : DbContext
    {
        public DbSet<Organization> Organization { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<UserType> UserType { get; set; }
        public DbSet<PasswordReset> PasswordReset { get; set; }

        public MySqlDbContext(DbContextOptions<MySqlDbContext> options)
           : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Organization>()
                .HasIndex(o => o.RouteName)
                .IsUnique();

            modelBuilder.Entity<User>()
               .HasIndex(u => u.UserName)
               .IsUnique();
        }

        public DbSet<EQLWebAPIWithAngular.Models.Login> Login { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseMySQL(
        //        @"Server=eqlb.weplayvr.com;Database=chlaanalytics;Uid=chlauser;Pwd=Cgh!2us3r@34Uiidw;");
        //}
    }
}
