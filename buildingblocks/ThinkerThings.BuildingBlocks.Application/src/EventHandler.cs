using MediatR;
using Microsoft.Extensions.Logging;

namespace ThinkerThings.BuildingBlocks.Application
{
    public abstract class EventHandler : Handler
    {
        protected EventHandler(IMediator mediator, ILogger logger)
            : base(mediator, logger)
        {
        }
    }
}