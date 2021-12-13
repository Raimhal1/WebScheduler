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
        public async Task<ActionResult<EventListVm>> GetEvents(int skip = 0, int take = 25)
        {
            var query = new GetEventListQuery
            {
                UserId = UserId,
                Skip = skip,
                Take = take
            };
            var vm = await Mediator.Send(query);
            return Ok(vm.Events);
        }

        [HttpGet]
        [Route("api/events")]
        public async Task<ActionResult<EventListVm>> GetEventsMember(int skip = 0, int take = 25)
        {
            var query = new GetEventListQueryMember
            {
                UserId = UserId,
                Skip = skip,
                Take = take
            };
            var vm = await Mediator.Send(query);
            return Ok(vm.Events);
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
        [Route("api/events/{eventId}/assign")]
        public async Task<IActionResult> AssignUserToEvent(Guid eventId)
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
        [Route("api/events/{eventId}/assign/{userId}")]
        public async Task<IActionResult> AssignUserToEventById(Guid eventId, Guid userId)
        {
            var command = new AssignUserCommand
            {
                UserId = userId,
                EventId = eventId
            };
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPut]
        [Route("api/events/assign")]
        public async Task<IActionResult> AssignUserToEventByEmail(AssignUserByEmailCommand command)
        {
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
