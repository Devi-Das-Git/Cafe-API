using MediatR;

namespace CafeAPI.Application.Commands
{


    public record RemoveEmployeeCommand(
     Guid UserId

     ) : IRequest<bool>;

}
