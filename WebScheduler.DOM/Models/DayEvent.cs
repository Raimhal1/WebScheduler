using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebScheduler.Domain.Interfaces;

namespace WebScheduler.Domain.Models
{
    public class DayEvent : IEntity<Guid>
    {
        public Guid Id { get; set; }

        public string EventName { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime StartEventDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime EndEventDate { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public Guid? CreatorId { get; set; }

        public virtual ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}
