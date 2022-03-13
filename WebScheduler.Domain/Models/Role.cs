using System.Collections.Generic;
using WebScheduler.Domain.Interfaces;

namespace WebScheduler.Domain.Models
{
    public class Role : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
    }
}
