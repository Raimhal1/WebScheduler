using System;
using WebScheduler.Domain.Interfaces;

namespace WebScheduler.Domain.Models
{
    public class Report : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }

    }
}
