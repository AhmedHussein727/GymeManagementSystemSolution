using GymeManagementDAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GymeManagementDAL.Data.Contexts
{
    public class GymeDbContext :IdentityDbContext<ApplicationUser>
    {
        public GymeDbContext(DbContextOptions<GymeDbContext>options):base(options)
        {
            
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=ZBOOK ; Database = GymeManagement ; Trusted_Connection=true;TrustServerCertificate=true");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<ApplicationUser>(eb =>
            {
                eb.Property(x=>x.FirstName).HasColumnType("varchar").HasMaxLength(50);
                eb.Property(x => x.LastName).HasColumnType("varchar").HasMaxLength(50);


            });
        }

        #region DbSets
        public DbSet<Member>Members { get; set; }
        public DbSet<HealthRecord> HealthRecords { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Sessions> Sessions { get; set; }
        public DbSet<MemberShip> MemberShips { get; set; }
        public DbSet<MemberSessions> MemberSessions { get; set; }
        #endregion
    }
}
