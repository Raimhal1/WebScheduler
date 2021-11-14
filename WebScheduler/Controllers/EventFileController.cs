using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpGet]
        [Route("api/events/{eventId}/files/{id}")]
        public async Task<IActionResult> GetEventFile(Guid id, Guid eventId)
        {
            if(await _assesService.HasAssesToEventFile(UserId, id)){

                var file = await _eventFileService.GetFile(id, eventId);
                return File(file.Content, file.ContentType);
            }
            return StatusCode(401);
        }


        [HttpPut]
        [Route("api/events/{eventId}/files/{id}/change-name")]
        public async Task<IActionResult> ChangeFileName(Guid id, Guid eventId,[FromForm] string Name, CancellationToken cancellationToken)
        {

            if (await _assesService.HasAssesToEvent(UserId, eventId))
            {
                await _eventFileService.ChangeFileName(id, eventId, Name, cancellationToken);
                return NoContent();
            }

            return StatusCode(401);
        }

        [HttpDelete]
        [Route("api/events/{eventId}/files/{id}/delete")]
        public async Task<IActionResult> DeleteFileFromEvent(Guid id, Guid eventId, CancellationToken cancellationToken)
        {
            if(await _assesService.HasAssesToEvent(UserId, eventId))
            {
                await _eventFileService.DeleteFileFromEvent(id, eventId, cancellationToken);
                return NoContent();
            }
            return StatusCode(401);
        }


        [HttpDelete]
        [Authorize(Roles ="Admin")]
        [Route("api/events/files/{id}/delete")]
        public async Task<IActionResult> DeleteFile(Guid id, CancellationToken cancellationToken)
        {
            if (await _assesService.HasAssesToEventFile(UserId, id))
            {
                await _eventFileService.DeleteFile(id, cancellationToken);
                return NoContent();
            }
            return StatusCode(401);
        }


    }
}
