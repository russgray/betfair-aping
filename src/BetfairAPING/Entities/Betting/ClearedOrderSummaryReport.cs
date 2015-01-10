using System.Collections.Generic;
using RestSharp.Serializers;

namespace BetfairAPING.Entities.Betting
{
    public class ClearedOrderSummaryReport
    {
        public List<ClearedOrderSummary> ClearedOrders { get; set; }
        public bool MoreAvailable { get; set; }

        public override string ToString()
        {
            var s = new JsonSerializer();
            return s.Serialize(this);
        }
    }
}