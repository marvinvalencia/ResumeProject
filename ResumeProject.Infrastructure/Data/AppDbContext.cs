// <copyright file="AppDbContext.cs" company="marvinvalencia">
// Copyright (c) marvinvalencia. All rights reserved.
// </copyright>

namespace ResumeProject.Infrastructure.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using ResumeProject.Domain.Entities;

    /// <summary>
    /// The AppDbContext class represents the database context for the application, inheriting from IdentityDbContext.
    /// </summary>
    public class AppDbContext : IdentityDbContext<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the DbSet for Resume entities.
        /// </summary>
        public DbSet<Resume> Resume { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for Experience entities.
        /// </summary>
        public DbSet<Experience> Experience { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for Education entities.
        /// </summary>
        public DbSet<Education> Education { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for Skill entities.
        /// </summary>
        public DbSet<Skill> Skill { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for User entities.
        /// </summary>
        public DbSet<User> User { get; set; }

        /// <summary>
        /// The OnConfiguring method is used to configure the database context options.
        /// </summary>
        /// <param name="optionsBuilder">The options builder.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        /// <summary>
        /// The OnModelCreating method is used to configure the model and relationships for the entities in the database context.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
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
    }
}
