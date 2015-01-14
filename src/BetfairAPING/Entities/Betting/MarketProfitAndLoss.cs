using System.Collections.Generic;

namespace BetfairAPING.Entities.Betting
{
    [ToString]
    public class MarketProfitAndLoss
    {
        public string MarketId { get; set; }
        public double CommissionApplied { get; set; }
        public List<RunnerProfitAndLoss> ProfitAndLosses { get; set; }
    }
}