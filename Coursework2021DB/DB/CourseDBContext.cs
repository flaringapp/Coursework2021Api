using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Coursework2021DB.DB
{
    public partial class CourseDBContext : DbContext
    {
        public CourseDBContext()
        {
        }

        public CourseDBContext(DbContextOptions<CourseDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<ManagerLocation> ManagerLocations { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<RoomRental> RoomRentals { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserLocation> UserLocations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:coursework-sql-sever.database.windows.net,1433;Initial Catalog=CourseDB;Persist Security Info=False;User ID=mallivance;Password=Aa123456;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.Area).HasColumnName("area");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.GeoLat).HasColumnName("geo_lat");

                entity.Property(e => e.GeoLon).HasColumnName("geo_lon");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Manager>(entity =>
            {
                entity.ToTable("Manager");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(1024)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(1024)
                    .HasColumnName("last_name");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.TimeCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("time_created");

                entity.Property(e => e.TimeUpdated)
                    .HasColumnType("datetime")
                    .HasColumnName("time_updated");
            });

            modelBuilder.Entity<ManagerLocation>(entity =>
            {
                entity.HasKey(e => e.ManagerId);

                entity.Property(e => e.ManagerId)
                    .ValueGeneratedNever()
                    .HasColumnName("manager_id");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.ManagerLocations)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_ManagerLocations_Locations");

                entity.HasOne(d => d.Manager)
                    .WithOne(p => p.ManagerLocation)
                    .HasForeignKey<ManagerLocation>(d => d.ManagerId)
                    .HasConstraintName("FK_ManagerLocations_Manager");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Area).HasColumnName("area");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.HasBalcony).HasColumnName("has_balcony");

                entity.Property(e => e.HasBoard).HasColumnName("has_board");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.PlacePrice).HasColumnName("place_price");

                entity.Property(e => e.PlacesCount).HasColumnName("places_count");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("type");

                entity.Property(e => e.WindowCount).HasColumnName("window_count");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_Rooms_Locations");
            });

            modelBuilder.Entity<RoomRental>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DatePaidUntil)
                    .HasColumnType("date")
                    .HasColumnName("date_paid_until");

                entity.Property(e => e.DateStart)
                    .HasColumnType("date")
                    .HasColumnName("date_start");

                entity.Property(e => e.RoomId).HasColumnName("room_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.RoomRentals)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK_RoomRentals_Rooms");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RoomRentals)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_RoomRentals_Users");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasIndex(e => e.Amount, "NCINX_Transactions_Date");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.DatePaidFrom)
                    .HasColumnType("date")
                    .HasColumnName("date_paid_from");

                entity.Property(e => e.DatePaidTo)
                    .HasColumnType("date")
                    .HasColumnName("date_paid_to");

                entity.Property(e => e.ManagerId).HasColumnName("manager_id");

                entity.Property(e => e.RentId).HasColumnName("rent_id");

                entity.Property(e => e.TimeCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("time_created");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.ManagerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transactions_Manager");

                entity.HasOne(d => d.Rent)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.RentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transactions_RoomRentals");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(1024)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(1024)
                    .HasColumnName("last_name");

                entity.Property(e => e.TimeCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("time_created");

                entity.Property(e => e.TimeUpdated)
                    .HasColumnType("datetime")
                    .HasColumnName("time_updated");
            });

            modelBuilder.Entity<UserLocation>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId)
                    .ValueGeneratedNever()
                    .HasColumnName("user_id");

                entity.Property(e => e.LocationId).HasColumnName("location_id");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.UserLocations)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_UserLocations_Locations");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.UserLocation)
                    .HasForeignKey<UserLocation>(d => d.UserId)
                    .HasConstraintName("FK_UserLocations_Users");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
