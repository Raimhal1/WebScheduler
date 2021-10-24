using System;
using Microsoft.EntityFrameworkCore;
using WebScheduler.Domain.Models;
using WebScheduler.Domain.Interfaces;
using WebScheluder.DAL.EntityTypeConfigurations;

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

            modelBuilder.ApplyConfiguration(new EventConfiguration());

            base.OnModelCreating(modelBuilder);

            string adminRoleName = "admin";
            string userRoleName = "user";

            string adminEmail = "admin@gmail.com";
            string adminPassword = "admin";

            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role userRole = new Role { Id = 2, Name = userRoleName };
            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });

            User adminUser = new User { Id = Guid.NewGuid(), UserName = "Admin", Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id };
            modelBuilder.Entity<User>().HasData(new User[] { adminUser });

        }
    }
}

