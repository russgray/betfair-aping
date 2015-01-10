using CommandLine;

namespace BetfairAPING.Console.Options
{
    class MarketFilterSubOptions : CommonOptions
    {
        [Option("text-query")]
        public string TextQuery { get; set; }

        [Option("event-type-ids")]
        public string EventTypeIds { get; set; }

        [Option("event-ids")]
        public string EventIds { get; set; }

        [Option("competition-ids")]
        public string CompetitionIds { get; set; }

        [Option("market-ids")]
        public string MarketIds { get; set; }
    }
}