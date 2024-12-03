using MediatR;
using Cafe.Domain.Repository;
using Cafe.Domain.Models;
using System.Net;

namespace CafeAPI.Application.Commands
{
    public class CreateCafeCommandHandler : IRequestHandler<CreateCafeCommand, bool>
    //IRequestHandler<CreateCafeCommand, bool>
    {
        private readonly ICafeRepository _repository;
        public CreateCafeCommandHandler(ICafeRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> Handle(CreateCafeCommand message, CancellationToken cancellationToken)
        {
            Cafe.Domain.Models.Cafe cafe = new Cafe.Domain.Models.Cafe() { Description = message.Description, Location = message.Location, Logo = message.Logo, Name = message.Name };
            
            _repository.Add(cafe);
            return Task.FromResult(true);
           
        }
    }
}
