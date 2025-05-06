﻿using System;
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

        public virtual DbSet<Users> Users { get; set; } = null!;
        public virtual DbSet<EmployeeModel> EmployeeList { get; set; } = null!;
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Attendance> Attendance { get; set; }
        public DbSet<OvertimeRecord> OvertimeRecords { get; set; }
        public DbSet<Salary> Salaries { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

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
