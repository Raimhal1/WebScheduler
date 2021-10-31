﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebScheduler.Domain.Models;

namespace WebScheduler.Domain.Interfaces
{
    public interface IRoleDbContext : IBaseDbContext
    {
        DbSet<Role> Roles { get; set; }
    }
}

