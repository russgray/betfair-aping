using CommandLine;

namespace BetfairAPING.Console.Options
{
    class MarketFilterSubOptions : CommonOptions
    {
        [Option("text-query")]
        public string TextQuery { get; set; }

        [Option("event-type-ids")]
        public string EventTypeIds { get; set; }
    }
}