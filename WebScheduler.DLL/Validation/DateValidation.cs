using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScheduler.BLL.Validation
{
    public static class DateValidation
    {
        public static bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}
