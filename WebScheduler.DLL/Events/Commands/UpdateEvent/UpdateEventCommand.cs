using System;
using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Http;
using WebScheduler.Domain.Models;

namespace WebScheduler.BLL.Events.Commands.UpdateEvent
{
    public class UpdateEventCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public string EventName { get; set; }

        public DateTime StartEventDate { get; set; }

        public DateTime EndEventDate { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }
        public Status Status { get; set; }
        public IList<User> Users { get; set; }
    }
}
