using System.Collections.Generic;

namespace BetfairAPING.Entities.Betting
{
    [ToString]
    public class StartingPrices
    {
        public double NearPrice { get; set; }
        public double FarPrice { get; set; }
        public List<PriceSize> BackStakeTaken { get; set; }
        public List<PriceSize> LayLiabilityTaken { get; set; }
        // ReSharper disable once InconsistentNaming
        public double ActualSP { get; set; }
    }
}