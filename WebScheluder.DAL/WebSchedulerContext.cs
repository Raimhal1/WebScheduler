using System;
using Microsoft.EntityFrameworkCore;
using WebScheduler.Domain.Models;
using WebScheduler.Domain.Interfaces;
using WebScheluder.DAL.EntityTypeConfigurations;
using System.Collections.Generic;

namespace WebScheluder.DAL
{
    public class WebSchedulerContext : DbContext, IEventDbContext, IUserDbContext,
        IRoleDbContext, IFileDbContext, IAllowedFileTypeDbContext, IEventFileDbContext,
        IReportDbContext
    {
        public WebSchedulerContext(DbContextOptions<WebSchedulerContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventFile> EventFiles { get; set; }
        public DbSet<AllowedFileType> AllowedFileTypes { get; set; }
        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new EventTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RoleEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EventFileTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AllowedFileTypeTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ReportTypeConfiguration());

            base.OnModelCreating(modelBuilder);

        }
    }
}

