using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using WebScheduler.BLL.DtoModels;
using WebScheduler.BLL.Interfaces;

namespace WebScheduler.Controllers
{
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService) =>
            _userService = userService;


        [HttpGet]
        [Route("api/users")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserListVm>> GetUsers()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        [Route("api/users/{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<UserDto>> GetUser(Guid id)
        {

            var user = await _userService.GetByIdAsync(id);
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/users")]
        public async Task<ActionResult<Guid>> CreateUser([FromBody] RegisterUserModel model,
            CancellationToken cancellationToken)
        {
            var user = await _userService.CreateAsync(model, cancellationToken);
            return Ok(user);
        }

        [Authorize]
        [HttpPut("{id}")]
        [Route("api/users/{id}/update")]
        public async Task<IActionResult> UpdateUser(Guid id, RegisterUserModel model,
            CancellationToken cancellationToken)
        {
            await _userService.UpdateAsync(id, model, cancellationToken);
            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [Route("api/users/{id}/delete")]
        public async Task<IActionResult> DeleteUser(Guid id, CancellationToken cancellationToken)
        {
            await _userService.DeleteByIdAsync(id, cancellationToken);
            return NoContent();

        }

    }
}
