using MassTransit;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace ThinkerThings.Service.Orders.Tasks
{
    public class BackgroundServiceConsumers : BackgroundService
    {
        private readonly IBusControl _busControl;

        public BackgroundServiceConsumers(IBusControl busControl)
        {
            _busControl = busControl;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _busControl.StartAsync(stoppingToken).ConfigureAwait(false);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await _busControl.StopAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}