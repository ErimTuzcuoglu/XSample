using System.Threading.Tasks;
using Application.Orchestrator;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace XSample.Common
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase<T> : ControllerBase where T : ApiControllerBase<T>
    {
        protected IActionResult ReturnRequestResult(Response requestResult)
        {
            if (requestResult.Succeeded)
            {
                return Ok(requestResult);
            }

            return BadRequest(requestResult);
        }
    }
}