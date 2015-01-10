using System.Collections.Generic;
using RestSharp.Serializers;

namespace BetfairAPING.Entities.Betting
{
    public class CurrentOrderSummaryReport
    {
        public List<CurrentOrderSummary> CurrentOrders { get; set; }
        public bool MoreAvailable { get; set; }

        public override string ToString()
        {
            var s = new JsonSerializer();
            return s.Serialize(this);
        }
    }
}