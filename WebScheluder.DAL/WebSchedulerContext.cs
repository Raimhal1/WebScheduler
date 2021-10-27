using System;
using Microsoft.EntityFrameworkCore;
using WebScheduler.Domain.Models;
using WebScheduler.Domain.Interfaces;
using WebScheluder.DAL.EntityTypeConfigurations;
using System.Collections.Generic;

namespace WebScheluder.DAL
{
    public class WebSchedulerContext : DbContext, IEventDbContext, IUserDbContext
    {
        public WebSchedulerContext(DbContextOptions<WebSchedulerContext> options)
            : base(options)
        {
            Database.EnsureCreated();

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new EventTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RoleEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RefreshTokenEntityTypeConfiguration());

            base.OnModelCreating(modelBuilder);

        }
    }
}

