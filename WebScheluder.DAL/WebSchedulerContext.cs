using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebScheduler.Domain.Models;


namespace WebScheduler.DAL.Data
{
    public class WebSchedulerContext : DbContext
    {
        public WebSchedulerContext(DbContextOptions<WebSchedulerContext> options)
            : base(options)
        {
            Database.EnsureCreated();

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<DayEvent> DayEvents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";
            string userRoleName = "user";

            string adminEmail = "admin@gmail.com";
            string adminPassword = "admin";

            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role userRole = new Role { Id = 2, Name = userRoleName };

            User adminUser = new User { Id = Guid.NewGuid(), UserName = "Admin", Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id };

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser });



            modelBuilder.Entity<DayEvent>()
                .Property(p => p.StartEventDate)
                .HasMaxLength(200)
                .IsRequired();
            modelBuilder.Entity<DayEvent>()
                .Property(p => p.EndEventDate)
                .IsRequired();
            modelBuilder.Entity<DayEvent>().
                Property(p => p.EventName)
                .IsRequired();
            modelBuilder.Entity<DayEvent>()
                .Property(p => p.ShortDescription)
                .HasMaxLength(30);

            modelBuilder.Entity<User>()
                .Property(p => p.UserName)
                .HasMaxLength(30);

        }
    }
}

