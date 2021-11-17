using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebScheduler.BLL.Validation;
using WebScheduler.Domain.Models;

namespace WebScheluder.DAL
{
    public class DbInitializer
    {
        public static void Initialize(WebSchedulerContext context)
        {
            using (context)
            {
                context.Database.EnsureCreated();
            }
        }

        public static void DataSeed(WebSchedulerContext context)
        {
            using (context)
            {
                if (!context.Users.Any())
                {
                    var adminRoleName = "Admin";
                    var userRoleName = "User";

                    var adminRole = context.Roles
                        .FirstOrDefault(r => r.Name == adminRoleName);

                    if (adminRole == null) {

                        adminRole = new Role
                        {
                            Id = 1,
                            Name = adminRoleName
                        };

                        var userRole = new Role
                        {
                            Id = 2,
                            Name = userRoleName
                        };
                        context.Roles.AddRangeAsync(adminRole, userRole);
                    }

                    var salt = Hasher.GenerateSalt(size: 16);

                    User admin = new User
                    {
                        Id = Guid.NewGuid(),
                        FirstName = "Admin",
                        LastName = "Admin",
                        UserName = "Admin",
                        Email = "admin_root@gmail.com",
                        Salt = salt,
                        Password = Hasher
                            .GetSaltedHash(password: "adminpass", salt: salt),
                        Roles = new List<Role> { adminRole }
                    };

                    context.Users.Add(admin);
                    context.SaveChanges();
                }
            }
        }
    }
}
