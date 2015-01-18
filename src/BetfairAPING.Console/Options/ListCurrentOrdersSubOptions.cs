using System.Collections.Generic;
using CommandLine;

namespace BetfairAPING.Console.Options
{
    class ListCurrentOrdersSubOptions : CommonOptions
    {
        [Option("bet-ids", HelpText = "Optionally restricts the results to the specified bet IDs")]
        public string BetIds { get; set; }

        [Option("market-ids", HelpText = "Optionally restricts the results to the specified market IDs")]
        public string MarketIds { get; set; }

        [Option("order-projection", HelpText = "Optionally restricts the results to the specified order status")]
        public string OrderProjection { get; set; }

        [Option("from-record", HelpText = "Specifies the first record that will be returned. Records start at index zero. If not specified then it will default to 0")]
        public int? FromRecord { get; set; }

        [Option("record-count", HelpText = "Specifies the maximum number of records to be returned. Note that there is a page size limit of 100")]
        public int? RecordCount { get; set; }
        
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