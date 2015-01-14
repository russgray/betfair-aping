using System.Collections.Generic;

namespace BetfairAPING.Entities.Betting
{
    [ToString]
    public class ClearedOrderSummaryReport
    {
        public List<ClearedOrderSummary> ClearedOrders { get; set; }
        public bool MoreAvailable { get; set; }
    }
}