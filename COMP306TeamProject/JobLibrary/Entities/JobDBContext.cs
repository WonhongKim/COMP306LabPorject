using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace JobLibrary.Entities
{
    public partial class JobDBContext : DbContext
    {
        public JobDBContext()
        {
        }

        public JobDBContext(DbContextOptions<JobDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Job> Job { get; set; }
        public virtual DbSet<JobRank> JobRank { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=database-1.cvooyrh672t3.us-east-2.rds.amazonaws.com;Database=JobDB; User ID=admin;Password=admin1234; Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Job>(entity =>
            {
                entity.Property(e => e.Discription)
                    .HasColumnName("discription")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.JobCategory)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.JobTitle)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<JobRank>(entity =>
            {
                entity.Property(e => e.BestLocations)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Rank)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.WorstLocations)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.JobRank)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK__JobRank__JobId__38996AB5");
            });
        }
    }
}
