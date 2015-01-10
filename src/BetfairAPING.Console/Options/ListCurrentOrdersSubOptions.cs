using System.Collections.Generic;
using CommandLine;

namespace BetfairAPING.Console.Options
{
    class ListCurrentOrdersSubOptions : CommonOptions
    {
        [Option("bet-ids")]
        public string BetIds { get; set; }

        [Option("market-ids")]
        public string MarketIds { get; set; }

        internal HashSet<string> BetIdsAsSet
        {
            get { return BetIds == null ? null : new HashSet<string>(BetIds.Split(',')); }
        }

        internal HashSet<string> MarketIdsAsSet
        {
            get { return MarketIds == null ? null : new HashSet<string>(MarketIds.Split(',')); }
        }
    }
}