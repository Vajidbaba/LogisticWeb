using System;
using System.Collections.Generic;
using Common.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Common.Data.Context
{
    public partial class LogisticContext : DbContext
    {
        private IContextHelper _contextHelper;
        public LogisticContext(DbContextOptions<LogisticContext> options, IContextHelper contextHelper): base(options)
        {
            _contextHelper = contextHelper;
        }

        public LogisticContext(DbContextOptions<LogisticContext> options) : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Users> Users { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.IdEmployee)
                    .HasName("PK__Employee__51C8DD7A4AF709B1");

                entity.ToTable("Employee");

                entity.HasIndex(e => e.Address, "UQ__Employee__7D0C3F32CCC652EE")
                    .IsUnique();

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Id)
                   .HasName("PK__Users__51C8DD7A4AF709B1");

                entity.ToTable("Users");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .HasColumnName("Username");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .HasColumnName("Mobile");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .HasColumnName("password");

                entity.Property(e => e.Role)
                    .HasMaxLength(20)
                    .HasColumnName("role");

            
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
