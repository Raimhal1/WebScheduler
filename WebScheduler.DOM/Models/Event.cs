using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebScheduler.Domain.Extentions;
using WebScheduler.Domain.Interfaces;

namespace WebScheduler.Domain.Models
{
    public class Event : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public string EventName { get; set; }

        public DateTime StartEventDate { get; set; }

        public DateTime EndEventDate { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }
        public Status Status => StartEventDate.UpdateStatus(EndEventDate);

        public Guid UserId { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<EventFile> EventFiles { get; set; }
    }

    public enum Status
    {
        Expected,
        InProgress,
        Ended
    }
}
