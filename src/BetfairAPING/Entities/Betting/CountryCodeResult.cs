using System.Diagnostics;

namespace BetfairAPING.Entities.Betting
{
    [DebuggerDisplay("CountryCode = {CountryCode}, MarketCount = {MarketCount}")]
    public class CountryCodeResult
    {
        public string CountryCode { get; set; }
        public int MarketCount { get; set; }
    }
}