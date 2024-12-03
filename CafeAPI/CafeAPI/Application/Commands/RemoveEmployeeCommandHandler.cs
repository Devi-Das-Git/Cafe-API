using Cafe.Domain.Repository;
using MediatR;

namespace CafeAPI.Application.Commands
{


    public class RemoveEmployeeCommandHandler : IRequestHandler<RemoveEmployeeCommand, bool>
    {
        private readonly ICafeRepository _repository;
        public RemoveEmployeeCommandHandler(ICafeRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> Handle(RemoveEmployeeCommand message, CancellationToken cancellationToken)
        {
            _repository.RemoveEmployee(message.UserId);

            return Task.FromResult(true);

        }
    }
}
