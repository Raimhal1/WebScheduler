using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScheduler.Domain.Interfaces;

namespace WebScheduler.Domain.Models
{
    public class EventFile : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public byte[] Data { get; set; }
        public Guid EventId { get; set; }
        public Event Event { get; set; }

    }
}
