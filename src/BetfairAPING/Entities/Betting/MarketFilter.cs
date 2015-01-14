using System.Collections.Generic;

namespace BetfairAPING.Entities.Betting
{
    [ToString]
    public class MarketFilter
    {
        public string TextQuery { get; set; }
        public HashSet<string> ExchangeIds { get; set; }
        public HashSet<string> EventTypeIds { get; set; }
        public HashSet<string> EventIds { get; set; } 
        public HashSet<string> CompetitionIds { get; set; }
        public HashSet<string> MarketIds { get; set; }
        public HashSet<string> Venues { get; set; }
        public bool BspOnly { get; set; }
        public bool TurnInPlayEnabled { get; set; }
        public bool InPlayOnly { get; set; }
        public HashSet<string> MarketBettingTypes { get; set; }
        public HashSet<string> MarketCountries { get; set; }
        public HashSet<string> MarketTypeCodes { get; set; }
        public TimeRange MarketStartTime { get; set; }
        public HashSet<string> WithOrders { get; set; }
    }
}