using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScheduler.Domain.Models;

namespace WebScheduler.BLL.Events.Commands.CreateEvent
{
    public class CreateEventCommand : IRequest<Guid>
    {
        public Guid UserId {get; set;}
        public string EventName { get; set; }

        public DateTime StartEventDate { get; set; }

        public DateTime EndEventDate { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }
        public Status Status { get; set; }
        public IList<User> Users { get; set; }
        public IList<IFormFile> files { get; set; }
    }
}
