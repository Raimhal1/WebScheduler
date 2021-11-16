using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WebScheduler.BLL.Interfaces;

namespace WebScheduler.Controllers
{
    [Authorize]
    public class EventFileController : BaseController
    {
        private readonly IEventFileService _eventFileService;
        private readonly IAccessService _assesService;

        public EventFileController(IEventFileService eventFileServvice, IAccessService assesService) =>
            (_eventFileService, _assesService) = (eventFileServvice, assesService);

        [HttpGet]
        [Route("api/events/{eventId}/files/{id}")]
        public async Task<IActionResult> GetEventFile(Guid eventId, Guid id)
        {
            if(await _assesService.HasAccessToEventFile(UserId, id)){

                var file = await _eventFileService.GetFile(id, eventId);
                return File(file.Content, file.ContentType);
            }
            return StatusCode(401);
        }

        [HttpPost]
        [Route("api/events/{eventId}/files/add-files")]
        public async Task<IActionResult> AddFileToEvent(Guid eventId, List<IFormFile> files, CancellationToken cancellationToken)
        {
            if(await _assesService.HasAccessToEvent(UserId, eventId))
            {
                await _eventFileService.AddFilesToEvent(eventId, files, cancellationToken);
                return Ok();
            }
            return StatusCode(403);
        }


        [HttpPut]
        [Route("api/events/{eventId}/files/{id}/change-name")]
        public async Task<IActionResult> ChangeFileName(Guid eventId, Guid id,[FromForm] string Name, CancellationToken cancellationToken)
        { 
            if (await _assesService.HasAccessToEvent(UserId, eventId))
            {
                await _eventFileService.ChangeFileName(id, eventId, Name, cancellationToken);
                return NoContent();
            }

            return StatusCode(403);
        }

        [HttpDelete]
        [Route("api/events/{eventId}/files/{id}/delete")]
        public async Task<IActionResult> DeleteFileFromEvent(Guid eventId, Guid id, CancellationToken cancellationToken)
        {
            if(await _assesService.HasAccessToEvent(UserId, eventId))
            {
                await _eventFileService.DeleteFileFromEvent(id, eventId, cancellationToken);
                return NoContent();
            }
            return StatusCode(403);
        }


        [HttpDelete]
        [Authorize(Roles ="Admin")]
        [Route("api/events/files/{id}/delete")]
        public async Task<IActionResult> DeleteFile(Guid id, CancellationToken cancellationToken)
        {
            if (await _assesService.HasAccessToEventFile(UserId, id))
            {
                await _eventFileService.DeleteFile(id, cancellationToken);
                return NoContent();
            }
            return StatusCode(403);
        }


    }
}
