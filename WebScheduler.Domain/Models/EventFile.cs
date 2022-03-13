using System;
using WebScheduler.Domain.Interfaces;

namespace WebScheduler.Domain.Models
{
    public class EventFile : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public Guid EventId { get; set; }
        public Event Event { get; set; }

    }
}
