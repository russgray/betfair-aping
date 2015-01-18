using System.Collections.Generic;
using CommandLine;

namespace BetfairAPING.Console.Options
{
    public class ListMarketProfitAndLossSubOptions : CommonOptions
    {
        [Option("market-ids", Required = true, HelpText = "List of markets to calculate profit and loss")]
        public string MarketIds { get; set; }

        [Option("include-settled-bets", DefaultValue = false, HelpText = "Option to include settled bets (partially settled markets only)")]
        public bool IncludeSettledBets { get; set; }

        [Option("include-bsp-bets", DefaultValue = false, HelpText = "Option to include BSP bets")]
        public bool IncludeBspBets { get; set; }

        [Option("net-of-commission", DefaultValue = false, HelpText = "Option to return profit and loss net of users current commission rate for this market including any special tariffs")]
        public bool NetOfCommission { get; set; }

        internal HashSet<string> MarketIdsAsSet
        {
            get { return MarketIds == null ? null : new HashSet<string>(MarketIds.Split(',')); }
        }
    }
}