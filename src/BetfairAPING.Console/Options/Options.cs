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

        [VerbOption("listevents")]
        public MarketFilterSubOptions ListEventsVerb { get; set; }

        [VerbOption("listeventtypes")]
        public MarketFilterSubOptions ListEventTypesVerb { get; set; }

        [VerbOption("listmarketbook")]
        public ListMarketBookSubOptions ListMarketBookVerb { get; set; }

        [HelpVerbOption]
        public string GetUsage(string verb)
        {
            return HelpText.AutoBuild(this, verb);
        }
    }

    class ListMarketBookSubOptions : CommonOptions
    {
        [Option("market-ids")]
        public string MarketIds { get; set; }

        [Option("price-projection")]
        public string PriceProjection { get; set; }

        [Option("order-projection")]
        public string OrderProjection { get; set; }

        [Option("match-projection")]
        public string MatchProjection { get; set; }
    
        internal HashSet<string> MarketIdsAsSet
        {
            get { return MarketIds == null ? null : new HashSet<string>(MarketIds.Split(',')); }
        }
    }
}