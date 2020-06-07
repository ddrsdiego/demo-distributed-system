using MediatR;
using Microsoft.Extensions.Logging;

namespace ThinkerThings.BuildingBlocks.Application
{
    public abstract class CommandHandler : Handler
    {
        protected CommandHandler(IMediator mediator, ILogger logger)
            : base(mediator, logger)
        {
        }
    }
}