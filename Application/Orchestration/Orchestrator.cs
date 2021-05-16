using System.Threading.Tasks;
using Application.Orchestrator;
using Domain.Core;
using MediatR;

namespace Application.Orchestration
{
    public class Orchestrator : IOrchestrator
    {
        private readonly IDomainNotifications _domainNotifications;
        private readonly IMediator _mediator;

        public Orchestrator(IMediator mediator, IDomainNotifications domainNotifications)
        {
            _mediator = mediator;
            _domainNotifications = domainNotifications;
        }

        public async Task<Response> SendCommand<T>(IRequest<T> request)
        {
            var commandResponse = await _mediator.Send(request);

            if (_domainNotifications.HasNotifications())
            {
                return new Response
                {
                    Succeeded = false,
                    Errors = _domainNotifications.GetAll()
                };
            }
            else
            {
                return new Response(commandResponse);
            }
        }

        public async Task<Response> SendQuery<T>(IRequest<T> request)
        {
            var commandResponse = await _mediator.Send(request);

            if (_domainNotifications.HasNotifications())
            {
                return new Response
                {
                    Succeeded = false,
                    Errors = _domainNotifications.GetAll()
                };
            }

            return new Response(commandResponse);
        }
    }
}