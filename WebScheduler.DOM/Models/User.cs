using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebScheduler.Domain.Interfaces;

namespace WebScheduler.Domain.Models
{
    public class User : IEntity<Guid>
    {
        public User()
        {
            if (Events == null)
            {
                Events = new List<Event>();
            }
        }
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}

