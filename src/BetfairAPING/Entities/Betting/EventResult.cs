namespace BetfairAPING.Entities.Betting
{
    [ToString]
    public class EventResult
    {
        public Event Event { get; set; }
        public int MarketCount { get; set; }
    }
}