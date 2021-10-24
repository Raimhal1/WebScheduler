using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebScheduler.Domain.Interfaces;

namespace WebScheduler.Domain.Models
{
    public class User : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}

