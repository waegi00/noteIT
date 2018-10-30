using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace NOTEit.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Mark> Marks { get; set; }

        public DbSet<Semester> Semesters { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new ApplicationDbInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Subject>()
                .HasRequired(s => s.Owner)
                .WithMany(o => o.Subjects)
                .WillCascadeOnDelete(true);
        }
    }
}