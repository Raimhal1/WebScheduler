using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScheluder.DAL
{
    public class DbInitializer
    {
        public static void Initialize(WebSchedulerContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
