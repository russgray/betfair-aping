using System.Collections.Generic;
using CommandLine;

namespace BetfairAPING.Console.Options
{
    class ListClearedOrdersSubOptions : CommonOptions
    {
        [Option("bet-status", Required = true, HelpText = "Restricts the results to the specified status")]
        public string BetStatus { get; set; }

        [Option("event-type-ids", HelpText = "Optionally restricts the results to the specified Event Type IDs")]
        public string EventTypeIds { get; set; }

        [Option("event-ids", HelpText = "Optionally restricts the results to the specified Event IDs")]
        public string EventIds { get; set; }

        [Option("market-ids", HelpText = "Optionally restricts the results to the specified market IDs")]
        public string MarketIds { get; set; }

        [Option("runner-ids", HelpText = "Optionally restricts the results to the specified Runners")]
        public string RunnerIds { get; set; }

        [Option("bet-ids", HelpText = "Optionally restricts the results to the specified bet IDs")]
        public string BetIds { get; set; }

        [Option("side", HelpText = "Optionally restricts the results to the specified side")]
        public string Side { get; set; }

        [Option("group-by", HelpText = "How to aggregate the lines, if not supplied then the lowest level is returned")]
        public string GroupBy { get; set; }

        [Option("include-item-description", HelpText = "If true then an ItemDescription object is included in the response")]
        public bool IncludeItemDescription { get; set; }

        [Option("from-record", HelpText = "Specifies the first record that will be returned. Records start at index zero. If not specified then it will default to 0")]
        public int? FromRecord { get; set; }

        [Option("record-count", HelpText = "Specifies the maximum number of records to be returned. Note that there is a page size limit of 100")]
        public int? RecordCount { get; set; }

        internal HashSet<string> EventTypeIdsAsSet
        {
            get { return EventTypeIds == null ? null : new HashSet<string>(EventTypeIds.Split(',')); }
        }

        internal HashSet<string> EventIdsAsSet
        {
            get { return EventIds == null ? null : new HashSet<string>(EventIds.Split(',')); }
        }

        internal HashSet<string> MarketIdsAsSet
        {
            get { return MarketIds == null ? null : new HashSet<string>(MarketIds.Split(',')); }
        }

        internal HashSet<string> RunnerIdsAsSet
        {
            get { return RunnerIds == null ? null : new HashSet<string>(RunnerIds.Split(',')); }
        }

        internal HashSet<string> BetIdsAsSet
        {
            get { return BetIds == null ? null : new HashSet<string>(BetIds.Split(',')); }
        }
    }
}