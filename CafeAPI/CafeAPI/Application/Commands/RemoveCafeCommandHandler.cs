using Cafe.Domain.Repository;
using MediatR;

namespace CafeAPI.Application.Commands
{
    public class RemoveCafeCommandHandler : IRequestHandler<RemoveCafeCommand, bool>
    {
        private readonly ICafeRepository _repository;
        public RemoveCafeCommandHandler(ICafeRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> Handle(RemoveCafeCommand message, CancellationToken cancellationToken)
        {
             _repository.RemoveCafe(message.UserId); 
            
            return Task.FromResult(true);
            
        }
    }
}
