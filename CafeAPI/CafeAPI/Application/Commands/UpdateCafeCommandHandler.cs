using Cafe.Domain.Repository;
using MediatR;

namespace CafeAPI.Application.Commands
{
    public class UpdateCafeCommandHandler : IRequestHandler<UpdateCafeCommand, bool>
    {
        private readonly ICafeRepository _repository;
        public UpdateCafeCommandHandler(ICafeRepository repository)
        {
            _repository = repository;
        }

       
        public Task<bool> Handle(UpdateCafeCommand message, CancellationToken cancellationToken)
        {
            Cafe.Domain.Models.Cafe cafe = new Cafe.Domain.Models.Cafe() { Description = message.Description, Location = message.Location, Logo = message.Logo, Name = message.Name,Id=message.UserId };

            _repository.UpdateCafe(cafe);
            return Task.FromResult(true);
            //return Task.FromResult(OrderDraftDTO.FromOrder(order));
        }

       
    }
}
