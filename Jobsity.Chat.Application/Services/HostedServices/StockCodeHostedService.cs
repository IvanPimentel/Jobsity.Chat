using Jobsity.Chat.Application.Handlers.Notifications.Stock;
using MediatR;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Jobsity.Chat.Application.Services.HostedServices
{
    public class StockCodeHostedService : BackgroundService
    {
        private readonly IMediator _mediator;

        public StockCodeHostedService(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // consumir RabbitMQ de retorno e criar mensagem

                    await Task.Delay(20 * 1000, stoppingToken);
                    await _mediator.Publish(new StockCodeProcessNotification("APPL.US quote is $93.42 per share", Guid.Parse("F632A1AF-CD05-4706-6E45-08DA310590DC")));
                }
                catch(Exception ex)
                {

                }

            }
        }
    }
}
