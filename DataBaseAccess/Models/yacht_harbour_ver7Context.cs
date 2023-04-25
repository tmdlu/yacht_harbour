using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataBaseAccess.Models
{
    public partial class yacht_harbour_ver7Context : DbContext
    {
        public yacht_harbour_ver7Context()
        {
        }

        public yacht_harbour_ver7Context(DbContextOptions<yacht_harbour_ver7Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Function> Functions { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Power> Powers { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<Yacht> Yachts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=Localhost;port=3306;database=yacht_harbour_ver7;user id=root;password=Test123456", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.10.2-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("cp1250_polish_ci")
                .HasCharSet("cp1250");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.IdAccount)
                    .HasName("PRIMARY");

                entity.ToTable("account");

                entity.Property(e => e.IdAccount)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_account");

                entity.Property(e => e.ClubStatus)
                    .HasMaxLength(128)
                    .HasColumnName("club_status");

                entity.Property(e => e.Email)
                    .HasMaxLength(128)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(64)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(64)
                    .HasColumnName("password");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(11)
                    .HasColumnName("phone_number");

                entity.Property(e => e.Photo)
                    .HasColumnType("blob")
                    .HasColumnName("photo");

                entity.Property(e => e.Role)
                    .HasMaxLength(64)
                    .HasColumnName("role");

                entity.Property(e => e.Surname)
                    .HasMaxLength(64)
                    .HasColumnName("surname");

                entity.HasMany(d => d.PowerIdPowers)
                    .WithMany(p => p.AccountIdAccounts)
                    .UsingEntity<Dictionary<string, object>>(
                        "PowerAssignment",
                        l => l.HasOne<Power>().WithMany().HasForeignKey("PowerIdPower").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fk_account_has_power_power1"),
                        r => r.HasOne<Account>().WithMany().HasForeignKey("AccountIdAccount").HasConstraintName("fk_account_has_power_account1"),
                        j =>
                        {
                            j.HasKey("AccountIdAccount", "PowerIdPower").HasName("PRIMARY").HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                            j.ToTable("power_assignment");

                            j.HasIndex(new[] { "AccountIdAccount" }, "fk_account_has_power_account1_idx");

                            j.HasIndex(new[] { "PowerIdPower" }, "fk_account_has_power_power1_idx");

                            j.IndexerProperty<int>("AccountIdAccount").HasColumnType("int(11)").HasColumnName("account_id_account");

                            j.IndexerProperty<int>("PowerIdPower").HasColumnType("int(11)").HasColumnName("power_id_power");
                        });
            });

            modelBuilder.Entity<Function>(entity =>
            {
                entity.HasKey(e => e.IdFunction)
                    .HasName("PRIMARY");

                entity.ToTable("function");

                entity.Property(e => e.IdFunction)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_function");

                entity.Property(e => e.FunctionName)
                    .HasMaxLength(128)
                    .HasColumnName("function_name");

                entity.HasMany(d => d.AccountIdAccounts)
                    .WithMany(p => p.FunctionIdFunctions)
                    .UsingEntity<Dictionary<string, object>>(
                        "Role",
                        l => l.HasOne<Account>().WithMany().HasForeignKey("AccountIdAccount").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fk_function_has_account_account1"),
                        r => r.HasOne<Function>().WithMany().HasForeignKey("FunctionIdFunction").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fk_function_has_account_function"),
                        j =>
                        {
                            j.HasKey("FunctionIdFunction", "AccountIdAccount").HasName("PRIMARY").HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                            j.ToTable("role");

                            j.HasIndex(new[] { "AccountIdAccount" }, "fk_function_has_account_account1_idx");

                            j.HasIndex(new[] { "FunctionIdFunction" }, "fk_function_has_account_function_idx");

                            j.IndexerProperty<int>("FunctionIdFunction").HasColumnType("int(11)").HasColumnName("function_id_function");

                            j.IndexerProperty<int>("AccountIdAccount").HasColumnType("int(11)").HasColumnName("account_id_account");
                        });
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.IdOrder)
                    .HasName("PRIMARY");

                entity.ToTable("order");

                entity.HasIndex(e => e.AccountIdAccount, "fk_order_account1_idx");

                entity.HasIndex(e => e.AccountIdAccount1, "fk_order_account2_idx");

                entity.HasIndex(e => e.AccountIdAccount2, "fk_order_account3_idx");

                entity.HasIndex(e => e.YachtIdYacht, "fk_order_yacht1_idx");

                entity.Property(e => e.IdOrder)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_order");

                entity.Property(e => e.AccountIdAccount)
                    .HasColumnType("int(11)")
                    .HasColumnName("account_id_account");

                entity.Property(e => e.AccountIdAccount1)
                    .HasColumnType("int(11)")
                    .HasColumnName("account_id_account1");

                entity.Property(e => e.AccountIdAccount2)
                    .HasColumnType("int(11)")
                    .HasColumnName("account_id_account2");

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("end_date");

                entity.Property(e => e.IsAccepted).HasColumnName("isAccepted");

                entity.Property(e => e.IsReleased).HasColumnName("isReleased");

                entity.Property(e => e.IsReturned).HasColumnName("isReturned");

                entity.Property(e => e.OrderType)
                    .HasMaxLength(64)
                    .HasColumnName("orderType");

                entity.Property(e => e.RequestDate)
                    .HasColumnType("datetime")
                    .HasColumnName("request_date");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("start_date");

                entity.Property(e => e.YachtIdYacht)
                    .HasColumnType("int(11)")
                    .HasColumnName("yacht_id_yacht");

                entity.HasOne(d => d.AccountIdAccountNavigation)
                    .WithMany(p => p.OrderAccountIdAccountNavigations)
                    .HasForeignKey(d => d.AccountIdAccount)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_order_account1");

                entity.HasOne(d => d.AccountIdAccount1Navigation)
                    .WithMany(p => p.OrderAccountIdAccount1Navigations)
                    .HasForeignKey(d => d.AccountIdAccount1)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_order_account2");

                entity.HasOne(d => d.AccountIdAccount2Navigation)
                    .WithMany(p => p.OrderAccountIdAccount2Navigations)
                    .HasForeignKey(d => d.AccountIdAccount2)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("fk_order_account3");

                entity.HasOne(d => d.YachtIdYachtNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.YachtIdYacht)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_order_yacht1");
            });

            modelBuilder.Entity<Power>(entity =>
            {
                entity.HasKey(e => e.IdPower)
                    .HasName("PRIMARY");

                entity.ToTable("power");

                entity.Property(e => e.IdPower)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_power");

                entity.Property(e => e.PowerName)
                    .HasMaxLength(128)
                    .HasColumnName("power_name");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.IdStatus)
                    .HasName("PRIMARY");

                entity.ToTable("status");

                entity.HasIndex(e => e.AccountIdAccount, "fk_status_account1_idx");

                entity.HasIndex(e => e.YachtIdYacht, "fk_status_yacht1_idx");

                entity.Property(e => e.IdStatus)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_status");

                entity.Property(e => e.AccountIdAccount)
                    .HasColumnType("int(11)")
                    .HasColumnName("account_id_account");

                entity.Property(e => e.Conditon)
                    .HasMaxLength(64)
                    .HasColumnName("conditon");

                entity.Property(e => e.SailingPossibility).HasColumnName("sailing_possibility");

                entity.Property(e => e.StatusDate)
                    .HasColumnType("datetime")
                    .HasColumnName("status_date");

                entity.Property(e => e.YachtIdYacht)
                    .HasColumnType("int(11)")
                    .HasColumnName("yacht_id_yacht");

                entity.HasOne(d => d.AccountIdAccountNavigation)
                    .WithMany(p => p.Statuses)
                    .HasForeignKey(d => d.AccountIdAccount)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_status_account1");

                entity.HasOne(d => d.YachtIdYachtNavigation)
                    .WithMany(p => p.Statuses)
                    .HasForeignKey(d => d.YachtIdYacht)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_status_yacht1");
            });

            modelBuilder.Entity<Yacht>(entity =>
            {
                entity.HasKey(e => e.IdYacht)
                    .HasName("PRIMARY");

                entity.ToTable("yacht");

                entity.Property(e => e.IdYacht)
                    .HasColumnType("int(11)")
                    .HasColumnName("id_yacht");

                entity.Property(e => e.CrewNumber)
                    .HasColumnType("int(11)")
                    .HasColumnName("crew_number");

                entity.Property(e => e.Draft).HasColumnName("draft");

                entity.Property(e => e.IsAvailable).HasColumnName("isAvailable");

                entity.Property(e => e.Length).HasColumnName("length");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(256)
                    .HasColumnName("remarks");

                entity.Property(e => e.SailedSurface).HasColumnName("sailed_surface");

                entity.Property(e => e.Type)
                    .HasMaxLength(128)
                    .HasColumnName("type");

                entity.Property(e => e.Width).HasColumnName("width");

                entity.Property(e => e.YachtName)
                    .HasMaxLength(128)
                    .HasColumnName("yacht_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
