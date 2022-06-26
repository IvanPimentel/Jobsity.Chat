using Jobsity.Chat.CrossCutting.Broker.Model;
using Jobsity.Chat.StooqService.Model;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Jobsity.Chat.StooqService.Handlers.Notifications.StockNotification
{
    public class StockRequestNotificationHandler : INotificationHandler<StockRequestNotification>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<StockRequestNotificationHandler> _logger;

        public StockRequestNotificationHandler(IMediator mediator, ILogger<StockRequestNotificationHandler> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Handle(StockRequestNotification notification, CancellationToken cancellationToken)
        {
            if (!cancellationToken.IsCancellationRequested)
            {
                var chatMessage = JsonConvert.DeserializeObject<ChatMessageBroker>(notification.Message);
                
                if (!chatMessage.Message.Contains("/stock="))
                    throw new Exception("Invalid Command");

                string stockCode = chatMessage.Message.Replace("/stock=","").Trim().ToLower();
                var url = $"https://stooq.com/q/l/?s={stockCode}&f=sd2t2ohlcv&h&e=csv";

                using (HttpClient client = new HttpClient())
                {
                    _logger.LogInformation($"Trying to get stock code: {stockCode} in stooq API");
                    HttpResponseMessage response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        _logger.LogInformation($"Success on get stock code: {stockCode} in stooq API");
                        _logger.LogInformation($"Start to processing CSV");
                        var stock = await ProccessCsv(response);
                        _logger.LogInformation($"CSV processed with success");
                        await _mediator.Publish(new StockResponseNotification(stock, chatMessage));
                    }
                    else
                    {
                        _logger.LogError($"Failure to process stock code: {stockCode}");
                    }
                }
            }
        }
        private static async Task<Stock> ProccessCsv(HttpResponseMessage response)
        {
            Stock stock = null;
            using (var invoiceStream = await response.Content.ReadAsStreamAsync())
            {
                using (TextFieldParser textFieldParser = new TextFieldParser(invoiceStream))
                {
                    textFieldParser.TextFieldType = FieldType.Delimited;
                    textFieldParser.SetDelimiters(",");
                    while (!textFieldParser.EndOfData)
                    {
                        if (textFieldParser.LineNumber == 2)
                        {
                            string[] rows = textFieldParser.ReadFields();
                            stock = new Stock(rows[0], rows[1], rows[2], rows[3], rows[4], rows[5], rows[6], rows[7]);
                        }
                        else
                            textFieldParser.ReadLine();
                    }
                }
            }
            return stock;
        }
    }
}
