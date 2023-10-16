using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PropertyRental.Models
{
    public partial class PropertyRentalDBContext : DbContext
    {
        public PropertyRentalDBContext()
        {
        }

        public PropertyRentalDBContext(DbContextOptions<PropertyRentalDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<Apartment> Apartments { get; set; } = null!;
        public virtual DbSet<ApartmentImage> ApartmentImages { get; set; } = null!;
        public virtual DbSet<Appointment> Appointments { get; set; } = null!;
        public virtual DbSet<Building> Buildings { get; set; } = null!;
        public virtual DbSet<Event> Events { get; set; } = null!;
        public virtual DbSet<EventType> EventTypes { get; set; } = null!;
        public virtual DbSet<Login> Logins { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<MessageStatus> MessageStatuses { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.Country).HasMaxLength(50);

                entity.Property(e => e.PostalCode).HasMaxLength(10);

                entity.Property(e => e.Province).HasMaxLength(50);

                entity.Property(e => e.StreetName).HasMaxLength(100);

                entity.Property(e => e.StreetNumber).HasMaxLength(20);
            });

            modelBuilder.Entity<Apartment>(entity =>
            {
                entity.Property(e => e.ApartmentId).HasColumnName("ApartmentID");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.BuildingId).HasColumnName("BuildingID");

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.PropertyManagerId).HasColumnName("PropertyManagerID");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");
            });

            modelBuilder.Entity<ApartmentImage>(entity =>
            {
                entity.HasKey(e => e.ImageId)
                    .HasName("PK__Apartmen__7516F4EC835C8DE2");

                entity.Property(e => e.ImageId).HasColumnName("ImageID");

                entity.Property(e => e.ApartmentId).HasColumnName("ApartmentID");

                entity.Property(e => e.ImageUrl).HasColumnName("ImageURL");

                entity.HasOne(d => d.Apartment)
                    .WithMany(p => p.ApartmentImages)
                    .HasForeignKey(d => d.ApartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Apartment__Apart__36B12243");
            });

            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.Property(e => e.AppointmentId).HasColumnName("AppointmentID");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.PropertyManagerId).HasColumnName("PropertyManagerID");

                entity.Property(e => e.TenantId).HasColumnName("TenantID");

                entity.Property(e => e.Timestamp).HasColumnType("datetime");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Appointme__Addre__3D5E1FD2");

                entity.HasOne(d => d.PropertyManager)
                    .WithMany(p => p.AppointmentPropertyManagers)
                    .HasForeignKey(d => d.PropertyManagerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Appointme__Prope__3B75D760");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.AppointmentTenants)
                    .HasForeignKey(d => d.TenantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Appointme__Tenan__3C69FB99");
            });

            modelBuilder.Entity<Building>(entity =>
            {
                entity.Property(e => e.BuildingId).HasColumnName("BuildingID");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");
            });

            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.Property(e => e.ApartmentId).HasColumnName("ApartmentID");

                entity.Property(e => e.EventTypeId).HasColumnName("EventTypeID");

                entity.Property(e => e.PropertyManagerId).HasColumnName("PropertyManagerID");

                entity.Property(e => e.PropertyOwnerId).HasColumnName("PropertyOwnerID");

                entity.Property(e => e.Timestamp).HasColumnType("datetime");

                entity.HasOne(d => d.Apartment)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.ApartmentId)
                    .HasConstraintName("FK__Events__Apartmen__46E78A0C");

                entity.HasOne(d => d.EventType)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.EventTypeId)
                    .HasConstraintName("FK__Events__EventTyp__47DBAE45");

                entity.HasOne(d => d.PropertyManager)
                    .WithMany(p => p.EventPropertyManagers)
                    .HasForeignKey(d => d.PropertyManagerId)
                    .HasConstraintName("FK__Events__Property__44FF419A");

                entity.HasOne(d => d.PropertyOwner)
                    .WithMany(p => p.EventPropertyOwners)
                    .HasForeignKey(d => d.PropertyOwnerId)
                    .HasConstraintName("FK__Events__Property__45F365D3");
            });

            modelBuilder.Entity<EventType>(entity =>
            {
                entity.Property(e => e.EventTypeId).HasColumnName("EventTypeID");

                entity.Property(e => e.EventTypeName).HasMaxLength(50);
            });

            modelBuilder.Entity<Login>(entity =>
            {
                entity.ToTable("Login");

                entity.Property(e => e.LoginId).HasColumnName("LoginID");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Password).HasMaxLength(100);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Logins)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Login__UserID__31EC6D26");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.Property(e => e.MessageStatusId).HasColumnName("MessageStatusID");

                entity.Property(e => e.ReceiverId).HasColumnName("ReceiverID");

                entity.Property(e => e.SenderId).HasColumnName("SenderID");

                entity.Property(e => e.Subject).HasMaxLength(100);

                entity.Property(e => e.Timestamp).HasColumnType("datetime");

                entity.HasOne(d => d.MessageStatus)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.MessageStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Messages__Messag__4222D4EF");

                entity.HasOne(d => d.Receiver)
                    .WithMany(p => p.MessageReceivers)
                    .HasForeignKey(d => d.ReceiverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Messages__Receiv__412EB0B6");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.MessageSenders)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Messages__Sender__403A8C7D");
            });

            modelBuilder.Entity<MessageStatus>(entity =>
            {
                entity.ToTable("MessageStatus");

                entity.Property(e => e.MessageStatusId).HasColumnName("MessageStatusID");

                entity.Property(e => e.Status).HasMaxLength(50);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.Property(e => e.StatusId)
                    .ValueGeneratedNever()
                    .HasColumnName("StatusID");

                entity.Property(e => e.StatusName).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Users__AddressID__2F10007B");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Users__RoleID__2E1BDC42");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
