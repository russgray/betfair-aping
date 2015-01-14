using System.Collections.Generic;
using CommandLine;

namespace BetfairAPING.Console.Options
{
    public class ListMarketProfitAndLossSubOptions : CommonOptions
    {
        [Option("market-ids", Required = true)]
        public string MarketIds { get; set; }

        [Option("include-settled-bets")]
        public bool IncludeSettledBets { get; set; }

        [Option("include-bsp-bets")]
        public bool IncludeBspBets { get; set; }

        [Option("net-of-commission")]
        public bool NetOfCommission { get; set; }

        internal HashSet<string> MarketIdsAsSet
        {
            get { return MarketIds == null ? null : new HashSet<string>(MarketIds.Split(',')); }
        }
    }
}