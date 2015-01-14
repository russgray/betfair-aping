using System;

namespace BetfairAPING.Entities.Betting
{
    [ToString]
    public class ItemDescription
    {
        public string EventTypeDesc { get; set; }
        public string EventDesc { get; set; }
        public string MarketDesc { get; set; }
        public DateTime MarketStartTime { get; set; }
        public string RunnerDesc { get; set; }
        public int NumberOfWinners { get; set; }
    }
}