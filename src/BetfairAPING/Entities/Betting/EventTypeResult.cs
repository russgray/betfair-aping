namespace BetfairAPING.Entities.Betting
{
    [ToString]
    public class EventTypeResult
    {
        public EventType EventType { get; set; }
        public int MarketCount { get; set; }
    }
}