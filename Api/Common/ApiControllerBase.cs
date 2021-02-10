using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace XSample.Common
{
    [ApiController]
    [Route("[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        private ISender _mediator;

        public ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
    }
}