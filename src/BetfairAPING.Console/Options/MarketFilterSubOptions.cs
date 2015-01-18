using BetfairAPING.Entities.Betting;
using CommandLine;

namespace BetfairAPING.Console.Options
{
    class MarketFilterSubOptions : CommonOptions
    {
        [Option("text-query", HelpText = "Restrict markets by any text associated with the market such as the Name, Event, Competition, etc")]
        public string TextQuery { get; set; }

        [Option("event-type-ids", HelpText = "Restrict markets by event type associated with the market. (i.e., Football, Hockey, etc)")]
        public string EventTypeIds { get; set; }

        [Option("event-ids", HelpText = "Restrict markets by the event id associated with the market")]
        public string EventIds { get; set; }

        [Option("competition-ids", HelpText = "Restrict markets by the competitions associated with the market")]
        public string CompetitionIds { get; set; }

        [Option("market-ids", HelpText = "Restrict markets by the market id associated with the market")]
        public string MarketIds { get; set; }

        [Option("venues", HelpText = "Restrict markets by the market id associated with the market")]
        public string Venues { get; set; }

        [Option("bsp-only", DefaultValue = null, HelpText = "Restrict to bsp markets only, if True or non-bsp markets if False. If not specified then returns both BSP and non-BSP markets")]
        public bool? BspOnly { get; set; }

        [Option("turn-inplay-enabled", DefaultValue = null, HelpText = "Restrict to markets that will turn in play if True or will not turn in play if false. If not specified, returns both")]
        public bool? TurnInPlayEnabled { get; set; }

        [Option("inplay-only", DefaultValue = null, HelpText = "Restrict to markets that are currently in play if True or are not currently in play if false. If not specified, returns both")]
        public bool? InPlayOnly { get; set; }

        public MarketFilter ToMarketFilter()
        {
            return new MarketFilter
            {
                TextQuery = TextQuery,
                EventTypeIds = CommandLineListToSet(EventTypeIds),
                EventIds = CommandLineListToSet(EventIds),
                CompetitionIds = CommandLineListToSet(CompetitionIds),
                MarketIds = CommandLineListToSet(MarketIds),
                Venues = CommandLineListToSet(Venues),
                BspOnly = BspOnly,
                TurnInPlayEnabled = TurnInPlayEnabled,
                InPlayOnly = InPlayOnly,
            };
        }
    }
}