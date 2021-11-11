using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScheduler.BLL.DtoModels
{
    public class GeneralLogData
    {
        public DateTime Time { get; set; }
        public string Data { get; set; }
        public Guid enitityId { get; set; }
    }
}
