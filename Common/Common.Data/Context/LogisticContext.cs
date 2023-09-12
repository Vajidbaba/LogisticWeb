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
        public virtual DbSet<Orders> Orders { get; set; } = null!;
        public virtual DbSet<DriverMaster> DriverMaster { get; set; } = null!;




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
