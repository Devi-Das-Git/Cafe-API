using MediatR;

namespace CafeAPI.Application.Commands
{
    public class IdentifiedCommandHandler<TCommand, TResponse> : IRequestHandler<IdentifiedCommand<TCommand, TResponse>, TResponse> where TCommand : IRequest<TResponse>
    {
        private readonly IMediator _mediator; 
        public IdentifiedCommandHandler(IMediator mediator) { _mediator = mediator; }
        public async Task<TResponse> Handle(IdentifiedCommand<TCommand, TResponse> request, CancellationToken cancellationToken)
        { 
            // Here you can implement any specific logic related to command identification
            return await _mediator.Send(request.Command, cancellationToken); 
        }

    }
}
