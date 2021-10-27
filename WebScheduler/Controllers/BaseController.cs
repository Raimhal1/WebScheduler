using System;
using System.Security.Claims;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

namespace WebScheduler.Controllers
{
    [ApiController]
    [Route("api/[cotroller]/[action]")]

    public abstract class BaseController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => 
            _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        internal Guid UserId => !User.Identity.IsAuthenticated ? Guid.Empty 
            : Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);


    }
}
