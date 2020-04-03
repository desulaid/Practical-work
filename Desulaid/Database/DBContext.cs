namespace Desulaid.Database
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=DBContext")
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Attendance> Attendance { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.Login)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.MiddleNam)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<Account>()
                .Property(e => e.GroupId)
                .IsUnicode(false);

            modelBuilder.Entity<Attendance>()
                .Property(e => e.Reason)
                .IsUnicode(false);
        }
    }
}
