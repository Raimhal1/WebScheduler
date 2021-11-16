using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebScheduler.BLL.Events.Commands.CreateEvent;
using WebScheduler.BLL.Events.Commands.UpdateEvent;
using WebScheduler.BLL.Events.Commands.DeleteEvent;
using WebScheduler.BLL.Events.Commands.AssignUser;
using WebScheduler.BLL.Events.Queries.GetEventDetails;
using WebScheduler.BLL.Events.Queries.GetEventList;
using WebScheduler.BLL.Events.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace WebScheduler.Controllers
{

    [Authorize]
    public class EventsController : BaseController
    {
        private readonly IMapper _mapper;

        public EventsController(IMapper mapper)
        {
            _mapper = mapper;
        }


        [HttpGet]
        [Route("api/my/events")]
        public async Task<ActionResult<EventListVm>> GetEvents()
        {
            var query = new GetEventListQuery
            {
                UserId = UserId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpGet]
        [Route("api/events")]
        public async Task<ActionResult<EventListVm>> GetEventsMember()
        {
            var query = new GetEventListQueryMember
            {
                UserId = UserId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }




        [HttpGet]
        [Route("api/events/{eventId}")]
        public async Task<ActionResult<EventDetailsVm>> GetEvent(Guid eventId)
        {
            var query = new GetEventDetailsQuery
            {
                UserId = UserId,
                Id = eventId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }


        [HttpPost]
        [Route("api/events")]
        public async Task<ActionResult<Guid>> CreateEvent([FromBody] CreateEventDto createEventDto)
        {
            var command = _mapper.Map<CreateEventCommand>(createEventDto);
            command.UserId = UserId;
            var eventId = await Mediator.Send(command);
            return Ok(eventId);
        }


        [HttpPut]
        [Route("api/events/{eventId}/update")]
        public async Task<IActionResult> UpdateEvent(Guid eventId, [FromBody] UpdateEventDto updateEventDto)
        {
            var command = _mapper.Map<UpdateEventCommand>(updateEventDto);
            command.UserId = UserId;
            command.Id = eventId;
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPut]
        [Route("api/events/{eventid}/assign")]
        public async Task<IActionResult> UpdateEvent(Guid eventId)
        {
            var command = new AssignUserCommand
            {
                UserId = UserId,
                EventId = eventId
            };
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPut]
        [Route("api/events/{eventid}/assign/{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEvent(Guid eventId, Guid userId)
        {
            var command = new AssignUserCommand
            {
                UserId = userId,
                EventId = eventId
            };
            await Mediator.Send(command);
            return NoContent();
        }



        [HttpDelete]
        [Route("api/events/{eventId}/delete")]
        public async Task<IActionResult> DeleteEvent(Guid eventId)
        {
            var command = new DeleteEventCommand
            {
                Id = eventId,
                UserId = UserId
            };
            await Mediator.Send(command);
            return NoContent();

        }
    }
}
