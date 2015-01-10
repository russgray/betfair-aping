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

        [HelpVerbOption]
        public string GetUsage(string verb)
        {
            return HelpText.AutoBuild(this, verb);
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