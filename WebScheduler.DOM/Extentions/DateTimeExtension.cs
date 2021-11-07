using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScheduler.Domain.Models;

namespace WebScheduler.Domain.Extentions
{
    public static class DateTimeExtension
    {
        public static Status UpdateStatus(this DateTime start, DateTime end)
        {
            var now = DateTime.UtcNow;
            if (now < start)
                return Status.Expected;
            else if (start <= now && now < end)
                return Status.InProgress;
            return Status.Ended;
        }
    }
}
