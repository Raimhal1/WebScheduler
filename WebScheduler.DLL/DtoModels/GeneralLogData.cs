using System;

namespace WebScheduler.BLL.DtoModels
{
    public class GeneralLogData
    {
        public DateTime Time { get; set; }
        public string Data { get; set; }
        public Guid enitityId { get; set; }
    }
}
