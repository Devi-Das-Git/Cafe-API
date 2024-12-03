using MediatR;

namespace CafeAPI.Application.Commands
{
   
    public record CreateEmployeeCommand(
    string Id,
    string Name,
    string Email,
    string Phone
    //string Gender

    ) : IRequest<bool>;
}
