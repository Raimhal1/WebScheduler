using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebScheduler.BLL.Interfaces;

namespace WebScheduler.Controllers
{
    [Authorize]
    public class EventFileController : BaseController
    {
        private readonly IEventFileService _eventFileService;
        private readonly IAssesService _assesService;

        public EventFileController(IEventFileService eventFileServvice, IAssesService assesService) =>
            (_eventFileService, _assesService) = (eventFileServvice, assesService);

        [HttpGet("id")]
        [Route("api/events/files/{id}")]
        public async Task<IActionResult> GetEventFile(Guid id)
        {
            if(_assesService.HasAssesToEventFile(UserId, id)){

                var file = await _eventFileService.GetFile(id);
                return File(file.Content, file.ContentType);
            }
            return StatusCode(401);
        }


        [HttpPut("{id}")]
        [Route("api/events/files/{id}/change-name")]
        public async Task<IActionResult> ChangeFileName(Guid id, [FromForm] string Name, CancellationToken cancellationToken)
        {

            if (_assesService.HasAssesToEventFile(UserId, id))
            {
                await _eventFileService.ChangeFileName(id, Name, cancellationToken);
                return NoContent();
            }

            return StatusCode(401);
        }

        [HttpDelete]
        [Route("api/events/{eventId}/files/{id}/delete")]
        public async Task<IActionResult> DeleteFileFromEvent(Guid id, Guid eventId, CancellationToken cancellationToken)
        {
            if(_assesService.HasAssesToEventFile(UserId, id))
            {
                await _eventFileService.DeleteFileFromEvent(id, eventId, cancellationToken);
                return NoContent();
            }
            return StatusCode(401);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles ="Admin")]
        [Route("api/events/files/{id}/delete")]
        public async Task<IActionResult> DeleteFile(Guid id, CancellationToken cancellationToken)
        {
            if (_assesService.HasAssesToEventFile(UserId, id))
            {
                await _eventFileService.DeleteFile(id, cancellationToken);
                return NoContent();
            }
            return StatusCode(401);
        }


    }
}
