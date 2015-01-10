using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;

namespace BetfairAPING.Console.Options
{
    class Options
    {
        [VerbOption("getaccountdetails")]
        public AccountDetailsSubOptions GetAccountDetailsVerb { get; set; }

        [VerbOption("getaccountfunds")]
        public AccountFundsSubOptions GetAccountFundsVerb { get; set; }

        [VerbOption("getaccountstatement")]
        public AccountStatementSubOptions GetAccountStatementVerb { get; set; }

        [VerbOption("listcurrencyrates")]
        public ListCurrencyRatesSubOptions ListCurrencyRatesVerb { get; set; }

        [VerbOption("listcompetitions")]
        public ListCompetitionsSubOptions ListCompetitionsVerb { get; set; }

        [VerbOption("listcountries")]
        public MarketFilterSubOptions ListCountriesVerb { get; set; }

        [VerbOption("listcurrentorders")]
        public ListCurrentOrdersSubOptions ListCurrentOrdersVerb { get; set; }

        [VerbOption("listclearedorders")]
        public ListClearedOrdersSubOptions ListClearedOrdersVerb { get; set; }

        [HelpVerbOption]
        public string GetUsage(string verb)
        {
            return HelpText.AutoBuild(this, verb);
        }
    }

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