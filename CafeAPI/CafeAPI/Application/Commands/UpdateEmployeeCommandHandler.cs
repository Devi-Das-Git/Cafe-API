using Cafe.Domain.Repository;
using MediatR;

namespace CafeAPI.Application.Commands
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, bool>
    {
        private readonly ICafeRepository _repository;
        public UpdateEmployeeCommandHandler(ICafeRepository repository)
        {
            _repository = repository;
        }

       
        public Task<bool> Handle(UpdateEmployeeCommand message, CancellationToken cancellationToken)
        {
            Cafe.Domain.Models.Employee employee = new Cafe.Domain.Models.Employee() { Id = message.Id, Email = message.Email, Phone = message.Phone, Name = message.Name, CafeId=message.CafeId, Gender =message.Gender, StartDate=message.StartDate };

            _repository.UpdateEmployee(employee);
            return Task.FromResult(true);
            
        }

       
    }
}
