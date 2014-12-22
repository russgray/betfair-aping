using System.Diagnostics;

namespace BetfairAPING.Entities.Betting
{
    [DebuggerDisplay("Competition = {Competition.Name}, MarketCount = {MarketCount}")]
    public class CompetitionResult
    {
        public Competition Competition { get; set; }
        public int MarketCount { get; set; }
        public string CompetitionRegion { get; set; }
    }
}