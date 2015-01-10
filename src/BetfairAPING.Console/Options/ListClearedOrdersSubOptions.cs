using System.Collections.Generic;
using CommandLine;

namespace BetfairAPING.Console.Options
{
    class ListClearedOrdersSubOptions : CommonOptions
    {
        [Option("bet-status", Required = true)]
        public string BetStatus { get; set; }

        [Option("event-type-ids")]
        public string EventTypeIds { get; set; }

        [Option("event-ids")]
        public string EventIds { get; set; }

        [Option("market-ids")]
        public string MarketIds { get; set; }

        [Option("runner-ids")]
        public string RunnerIds { get; set; }

        [Option("bet-ids")]
        public string BetIds { get; set; }

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