using CafeAPI.Application.Queries;
using MediatR;

namespace CafeAPI.Apis
{
    public class CafeServices(
        IMediator mediator,
        ICafeQueries queries,
        ILogger<CafeServices> logger)
    {
        public IMediator Mediator { get; set; } = mediator;
        public ILogger<CafeServices> Logger { get; } = logger;
        public ICafeQueries Queries { get; } = queries;
        
    }

}
