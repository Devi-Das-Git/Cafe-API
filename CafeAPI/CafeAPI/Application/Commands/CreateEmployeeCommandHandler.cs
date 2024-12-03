using MediatR;
using Cafe.Domain.Repository;
using Cafe.Domain.Models;
using System.Net;
using System;
using System.Runtime.InteropServices;

namespace CafeAPI.Application.Commands
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, bool>
    {
        private readonly ICafeRepository _repository;
        public CreateEmployeeCommandHandler(ICafeRepository repository)
        {
            _repository = repository;
        }

        public Task<bool> Handle(CreateEmployeeCommand message, CancellationToken cancellationToken)
        {
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        string id = new string(Enumerable.Range(1, 7).Select(_ => chars[random.Next(chars.Length)]).ToArray());

        string uuid = "UI" + id;
        Cafe.Domain.Models.Employee employee = new Cafe.Domain.Models.Employee() { Id = uuid, Email = message.Email, Phone = message.Phone, Name = message.Name,Gender="" };

        _repository.AddEmployee(employee);
            return Task.FromResult(true);
            //return Task.FromResult(OrderDraftDTO.FromOrder(order));
        }
    }
}
