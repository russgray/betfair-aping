using System.Collections.Generic;
using CommandLine;

namespace BetfairAPING.Console.Options
{
    class ListMarketBookSubOptions : CommonOptions
    {
        [Option("market-ids")]
        public string MarketIds { get; set; }

        [Option("price-projection")]
        public string PriceProjection { get; set; }

        [Option("order-projection")]
        public string OrderProjection { get; set; }

        [Option("match-projection")]
        public string MatchProjection { get; set; }
    
        internal HashSet<string> MarketIdsAsSet
        {
            get { return MarketIds == null ? null : new HashSet<string>(MarketIds.Split(',')); }
        }
    }
}