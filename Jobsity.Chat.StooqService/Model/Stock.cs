namespace Jobsity.Chat.StooqService.Model
{
    public class Stock
    {
        public string Symbol { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Open { get; set; }
        public string High { get; set; }
        public string Low { get; set; }
        public string Close { get; set; }
        public string Volume { get; set; }

        public Stock(string symbol, string date, string time, string open, string high, string low, string close, string volume)
        {
            Symbol = symbol;
            Date = date;
            Time = time;
            Open = open;
            High = high;
            Low = low;
            Close = close;
            Volume = volume;
        }
    }
}
