using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScheduler.BLL.Validation
{
    public static class Status
    {
        public static Domain.Models.Status ChangeStatus(DateTime startDate, DateTime endDate)
        {
            var now = DateTime.UtcNow;
            if (now < startDate)
                return Domain.Models.Status.Expected;
            else if (startDate <= now && now < endDate)
                return Domain.Models.Status.IpProgress;

            return Domain.Models.Status.Ended;
        }
    }
}
