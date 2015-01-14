using System;
using System.Collections.Generic;

namespace BetfairAPING.Entities.Betting
{
    [ToString]
    public class MarketBook
    {
        public string MarketId { get; set; }
        public bool IsMarketDataDelayed { get; set; }
        public string MarketStatus { get; set; }
        public int BetDelay { get; set; }
        public bool BspReconciled { get; set; }
        public bool Complete { get; set; }
        public bool InPlay { get; set; }
        public int NumberOfWinners { get; set; }
        public int NumberOfRunners { get; set; }
        public int NumberOfActiveRunners { get; set; }
        public DateTime LastMatchTime { get; set; }
        public double TotalMatched { get; set; }
        public double TotalAvailable { get; set; }
        public bool CrossMatching { get; set; }
        public bool RunnersVoidable { get; set; }
        public long Version { get; set; }
        public List<Runner> Runners { get; set; }
    }
}