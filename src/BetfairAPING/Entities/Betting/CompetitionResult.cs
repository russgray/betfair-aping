using System.Diagnostics;
using RestSharp.Serializers;

namespace BetfairAPING.Entities.Betting
{
    [DebuggerDisplay("Competition = {Competition.Name}, MarketCount = {MarketCount}")]
    public class CompetitionResult
    {
        public Competition Competition { get; set; }
        public int MarketCount { get; set; }
        public string CompetitionRegion { get; set; }

        public override string ToString()
        {
            var s = new JsonSerializer();
            return s.Serialize(this);
        }
    }
}