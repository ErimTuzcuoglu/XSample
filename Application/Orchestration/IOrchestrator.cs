using System.Threading.Tasks;
using MediatR;

namespace Application.Orchestrator
{
    public interface IOrchestrator
    {
        public Task<Response> SendCommand<T>(IRequest<T> request);
        public Task<Response> SendQuery<T>(IRequest<T> request);
    }
}