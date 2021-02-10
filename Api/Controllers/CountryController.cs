using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using XSample.Common;

namespace XSample.Controllers
{
    public class CountryController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return new JsonResult(new {Deneme = "test"});
        }
    }
}