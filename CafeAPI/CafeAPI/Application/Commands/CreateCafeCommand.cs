using MediatR;

namespace CafeAPI.Application.Commands
{
   
    public record CreateCafeCommand(
    string UserId,
    string Name,
    string Description,
     byte[] Logo,
    string Location
    
    ) : IRequest<bool>;
}
