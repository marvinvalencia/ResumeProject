using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using ResumeProject.Domain.Entities;

namespace ResumeProject.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Resume>().ToTable("Resume");
            modelBuilder.Entity<Experience>().ToTable("Experience");
            modelBuilder.Entity<Education>().ToTable("Education");
            modelBuilder.Entity<Skill>().ToTable("Skill");
            modelBuilder.Entity<User>().ToTable("User");

            // Configure relationships, indexes, etc. if needed
            modelBuilder.Entity<Resume>(entity =>
            {
                entity.HasKey(r => r.Id);

                entity.Property(r => r.Id)
                      .HasDefaultValueSql("NEWID()");

                entity.HasMany(r => r.Experiences)
                    .WithOne(e => e.Resume)
                    .HasForeignKey(e => e.ResumeId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(r => r.Educations)
                    .WithOne(e => e.Resume)
                    .HasForeignKey(e => e.ResumeId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(r => r.Skills)
                    .WithOne(s => s.Resume)
                    .HasForeignKey(s => s.ResumeId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Experience>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasDefaultValueSql("NEWID()");
            });

            modelBuilder.Entity<Education>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                      .HasDefaultValueSql("NEWID()");
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.HasKey(s => s.Id);

                entity.Property(s => s.Id)
                      .HasDefaultValueSql("NEWID()");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(u => u.Id)
                      .HasDefaultValueSql("NEWID()");
            });
        }

        public DbSet<Resume> Resume { get; set; }
        public DbSet<Experience> Experience { get; set; }
        public DbSet<Education> Education { get; set; }
        public DbSet<Skill> Skill { get; set; }
        public DbSet<User> User { get; set; }
    }
}
