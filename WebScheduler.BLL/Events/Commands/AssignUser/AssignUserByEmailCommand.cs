using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScheduler.BLL.Events.Commands.AssignUser
{
    public class AssignUserByEmailCommand : IRequest
    {
        public string Email { get; set; }
        public Guid EventId { get; set; }
    }
}
