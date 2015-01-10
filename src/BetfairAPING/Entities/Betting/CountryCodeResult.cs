using System.Diagnostics;
using RestSharp.Serializers;

namespace BetfairAPING.Entities.Betting
{
    [DebuggerDisplay("CountryCode = {CountryCode}, MarketCount = {MarketCount}")]
    public class CountryCodeResult
    {
        public string CountryCode { get; set; }
        public int MarketCount { get; set; }

        public override string ToString()
        {
            var s = new JsonSerializer();
            return s.Serialize(this);
        }
    }
}