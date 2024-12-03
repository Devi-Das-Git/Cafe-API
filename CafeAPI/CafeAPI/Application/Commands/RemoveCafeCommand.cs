using MediatR;

namespace CafeAPI.Application.Commands
{
    public record RemoveCafeCommand(
     Guid UserId
     
     ) : IRequest<bool>;
}
