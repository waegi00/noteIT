using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace NOTEit.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Mark> Marks { get; set; }

        public DbSet<Semester> Semesters { get; set; }

        public DbSet<Subject> Subjects { get; set; }

        public DbSet<WishMark> WishMarks { get; set; }

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

            modelBuilder.Entity<WishMark>()
                .HasMany(w => w.Marks)
                .WithOptional(m => m.WishMark)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<WishMark>()
                .HasRequired(w => w.Subject)
                .WithMany(s => s.WishMarks)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WishMark>()
                .HasRequired(w => w.Semester)
                .WithMany(s => s.WishMarks)
                .WillCascadeOnDelete(false);
        }
    }
}