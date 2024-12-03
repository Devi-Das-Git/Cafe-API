using MediatR;

namespace CafeAPI.Application.Commands
{
    public record UpdateEmployeeCommand(
    string Id,
    string Name,
    string Email,
    string Phone,
    string Gender

    ) : IRequest<bool>;
}
