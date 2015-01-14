using System.Collections.Generic;

namespace BetfairAPING.Entities.Betting
{
    [ToString]
    public class CurrentOrderSummaryReport
    {
        public List<CurrentOrderSummary> CurrentOrders { get; set; }
        public bool MoreAvailable { get; set; }
    }
}