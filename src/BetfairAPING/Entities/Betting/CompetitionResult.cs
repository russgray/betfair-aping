namespace BetfairAPING.Entities.Betting
{
    [ToString]
    public class CompetitionResult
    {
        public Competition Competition { get; set; }
        public int MarketCount { get; set; }
        public string CompetitionRegion { get; set; }
    }
}