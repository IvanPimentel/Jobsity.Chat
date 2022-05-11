using Jobsity.Chat.StooqService.Model;
using MediatR;
using System;

namespace Jobsity.Chat.StooqService.Handlers.Notifications.StockNotification
{
    public class StockResponseNotification : INotification
    {
        public Stock Stock { get; private set; }
        public string StockCode { get; set; }

        public bool Success { get; private set; }

        public StockResponseNotification(Stock stock, string stockCode)
        {
            Stock = stock;
            StockCode = stockCode;
            Validation();
        }

        private void Validation()
        {
            Success = Stock != null;
            if(Stock != null && Stock.Close == "N/D")
                Success = false;
        }
    }
}
