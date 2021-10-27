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
using WebScheduler.BLL.Events.Queries.GetEventDetails;
using WebScheduler.BLL.Events.Queries.GetEventList;
using WebScheduler.BLL.Events.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace WebScheduler.Controllers
{

    [Route("api/[controller]/[action]")]
    [Authorize(Roles = "Admin, User")]
    public class EventsController : BaseController
    {
        private readonly IMapper _mapper;



        public EventsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<EventListVm>> GetEvents()
        {
            var query = new GetEventListQuery
            {
                UserId = UserId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventDetailsVm>> GetEvent(Guid id)
        {
            var query = new GetEventDetailsQuery
            {
                UserId = UserId,
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }


        [HttpPost]
        public async Task<ActionResult<Guid>> CreateEvent([FromBody] CreateEventDto createEventDto)
        {
            var command = _mapper.Map<CreateEventCommand>(createEventDto);
            command.UserId = UserId;
            var eventId = await Mediator.Send(command);
            return Ok(eventId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent([FromBody] UpdateEventDto updateEventDto)
        {
            var command = _mapper.Map<UpdateEventCommand>(updateEventDto);
            command.UserId = UserId;
            await Mediator.Send(command);
            return NoContent();
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            var command = new DeleteEventCommand
            {
                Id = id,
                UserId = UserId

            };
            await Mediator.Send(command);
            return NoContent();

        }
    }
}
