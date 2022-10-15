using HrSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HrSystem.Infrastructure.Context
{
    public partial class HrSystemContext : DbContext
    {
        public HrSystemContext()
        {
        }

        public HrSystemContext(DbContextOptions<HrSystemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Candidate> Candidates { get; set; } = null!;
        public virtual DbSet<Requisition> Requisitions { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<RoleClaim> RoleClaims { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserClaim> UserClaims { get; set; } = null!;
        public virtual DbSet<UserLogin> UserLogins { get; set; } = null!;
        public virtual DbSet<UserToken> UserTokens { get; set; } = null!;
        public virtual DbSet<Vacancy> Vacancies { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                          .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                          .AddJsonFile("appsettings.json").Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            }
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidate>(entity =>
            {
                entity.ToTable("Candidate");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.Email).HasMaxLength(150);

                entity.Property(e => e.Mobile).HasColumnType("numeric(12, 0)");

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Candidates)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_Candidate_Users");
            });

            modelBuilder.Entity<Requisition>(entity =>
            {
                entity.ToTable("Requisition");

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.HasOne(d => d.Candidate)
                    .WithMany(p => p.Requisitions)
                    .HasForeignKey(d => d.CandidateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Requisition_Candidate");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Requisitions)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_Requisition_Users");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.Requisitions)
                    .HasForeignKey(d => d.Status)
                    .HasConstraintName("FK_Requisition_Status");

                entity.HasOne(d => d.Vacancy)
                    .WithMany(p => p.Requisitions)
                    .HasForeignKey(d => d.VacancyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Requisition_Vacancy");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<RoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_RoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status");

                entity.Property(e => e.StatusName)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.StatusValue)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserRole",
                        l => l.HasOne<Role>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("UserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_UserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<UserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_UserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<UserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_UserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<UserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Vacancy>(entity =>
            {
                entity.ToTable("Vacancy");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy).HasMaxLength(450);

                entity.Property(e => e.Department).HasMaxLength(150);

                entity.Property(e => e.JobName).HasMaxLength(150);

                entity.Property(e => e.Qualifications).HasMaxLength(550);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Vacancies)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_Vacancy_Users");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
