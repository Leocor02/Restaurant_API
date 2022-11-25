using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Restaurant_API.Models
{
    public partial class RestaurantContext : DbContext
    {
        public RestaurantContext()
        {
        }

        public RestaurantContext(DbContextOptions<RestaurantContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Currency> Currencies { get; set; } = null!;
        public virtual DbSet<Dish> Dishes { get; set; } = null!;
        public virtual DbSet<Invoice> Invoices { get; set; } = null!;
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; } = null!;
        public virtual DbSet<Reservation> Reservations { get; set; } = null!;
        public virtual DbSet<Table> Tables { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("SERVER=.\\SQLEXPRESS;DATABASE=Restaurant;INTEGRATED SECURITY=TRUE; User Id=;Password=");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.Idcountry)
                    .HasName("PK__Country__D9D5A69477BE5481");

                entity.ToTable("Country");

                entity.Property(e => e.Idcountry).HasColumnName("IDCountry");

                entity.Property(e => e.CountryName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.HasKey(e => e.Idcurrency)
                    .HasName("PK__Currency__5089073920B47943");

                entity.ToTable("Currency");

                entity.Property(e => e.Idcurrency).HasColumnName("IDCurrency");

                entity.Property(e => e.CurrencyCode)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.CurrencyName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Dish>(entity =>
            {
                entity.HasKey(e => e.Iddish)
                    .HasName("PK__Dish__52C21B926CB7F128");

                entity.ToTable("Dish");

                entity.Property(e => e.Iddish).HasColumnName("IDDish");

                entity.Property(e => e.DishDescription)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Idcountry).HasColumnName("IDCountry");

                entity.Property(e => e.ItemPictureUrl)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasColumnName("ItemPictureURL");

                entity.HasOne(d => d.IdcountryNavigation)
                    .WithMany(p => p.Dishes)
                    .HasForeignKey(d => d.Idcountry)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Region");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(e => e.Idinvoice)
                    .HasName("PK__Invoice__4DA85D701E0BD72A");

                entity.ToTable("Invoice");

                entity.Property(e => e.Idinvoice).HasColumnName("IDInvoice");

                entity.Property(e => e.Idcurrency).HasColumnName("IDCurrency");

                entity.Property(e => e.IdpaymentMethod).HasColumnName("IDPaymentMethod");

                entity.Property(e => e.Iduser).HasColumnName("IDUser");

                entity.Property(e => e.InvoiceDate).HasColumnType("date");

                entity.Property(e => e.InvoiceNotes)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.InvoiceNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdcurrencyNavigation)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.Idcurrency)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Currency");

                entity.HasOne(d => d.IdpaymentMethodNavigation)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.IdpaymentMethod)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_PaymentMethod");

                entity.HasOne(d => d.IduserNavigation)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.Iduser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_User");
            });

            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.HasKey(e => e.IdpaymentMethod)
                    .HasName("PK__PaymentM__2994821394D98D83");

                entity.ToTable("PaymentMethod");

                entity.Property(e => e.IdpaymentMethod).HasColumnName("IDPaymentMethod");

                entity.Property(e => e.PaymentMethodDescription)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => e.Idreservation)
                    .HasName("PK__Reservat__53DF2D8D133976BD");

                entity.ToTable("Reservation");

                entity.Property(e => e.Idreservation).HasColumnName("IDReservation");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Idtable).HasColumnName("IDTable");

                entity.Property(e => e.Iduser).HasColumnName("IDUser");

                entity.HasOne(d => d.IdtableNavigation)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.Idtable)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Tables");

                entity.HasOne(d => d.IduserNavigation)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.Iduser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Person");
            });

            modelBuilder.Entity<Table>(entity =>
            {
                entity.HasKey(e => e.Idtable)
                    .HasName("PK__Table__72B3DFF41F8AAA10");

                entity.ToTable("Table");

                entity.Property(e => e.Idtable).HasColumnName("IDTable");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Iduser)
                    .HasName("PK__User__EAE6D9DFDBA01A04");

                entity.ToTable("User");

                entity.Property(e => e.Iduser).HasColumnName("IDUser");

                entity.Property(e => e.BackUpEmail)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Idcountry).HasColumnName("IDCountry");

                entity.Property(e => e.IduserRole).HasColumnName("IDUserRole");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdcountryNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Idcountry)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Country");

                entity.HasOne(d => d.IduserRoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IduserRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_UserRole");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.IduserRole)
                    .HasName("PK__UserRole__5A7AF7818802D0A0");

                entity.ToTable("UserRole");

                entity.Property(e => e.IduserRole).HasColumnName("IDUserRole");

                entity.Property(e => e.Role)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
