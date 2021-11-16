﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using WebScheduler.BLL.DtoModels;
using WebScheduler.BLL.Interfaces;

namespace WebScheduler.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FileSettingsController : BaseController
    {
        private readonly IFileSettingsService _fileSettingsService;

        public FileSettingsController(IFileSettingsService fileSettingsService) =>
            _fileSettingsService = fileSettingsService;


        [HttpGet]
        [Route("api/file-settings/types")]
        public async Task<ActionResult<AllowedFileTypeListVm>> GetFileTypes()
        {
            var allowedFileTypes = await _fileSettingsService.GetAllowedFileTypes();
            return Ok(allowedFileTypes);
        }


        [HttpPost]
        [Route("api/file-settings/types/add")]
        public async Task<ActionResult<int>> AddFileType([FromBody] AllowedFileTypeDto fileTypeDto,
            CancellationToken cancellationToken)
        {
            var fileTypeId = await _fileSettingsService
                .AddFileType(fileTypeDto, cancellationToken);
            if (fileTypeId == default) 
                return Ok(new { message = $"The file type '{fileTypeDto.FileType}' already exists" });
            return Ok(fileTypeId);

        }

        [HttpPut]
        [Route("api/file-settings/types/{id}/update")]
        public async Task<IActionResult> ChangeFileType(int id, AllowedFileTypeDto fileTypeDto,
            CancellationToken cancellationToken)
        {
            await _fileSettingsService.ChangeFileType(id, fileTypeDto, cancellationToken);
            return NoContent();
        }

        [HttpDelete]
        [Route("api/file-settings/types/{id}/delete")]
        public async Task<IActionResult> DeleteFileType(int id, CancellationToken cancellationToken)
        {
            await _fileSettingsService.DeleteFileType(id, cancellationToken);
            return NoContent();
        }
    }
}
