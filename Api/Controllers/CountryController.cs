using System.Threading.Tasks;
using Application.Orchestrator;
using Domain.RequestHandlers.Countries.Queries.GetAll;
using Microsoft.AspNetCore.Mvc;
using XSample.Common;

namespace XSample.Controllers
{
    public class CountryController : ApiControllerBase<CountryController>
    {
        private IOrchestrator Orchestrator { get; set; }

        public CountryController(IOrchestrator orchestrator)
        {
            Orchestrator = orchestrator;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await Orchestrator.SendQuery(new GetCountriesQuery());
            return ReturnRequestResult(result);
        }
    }
}