using CommandLine;
using CommandLine.Text;

namespace BetfairAPING.Console.Options
{
    class Options
    {
        [VerbOption("getaccountdetails", HelpText = "Returns the details relating your account")]
        public CommonOptions GetAccountDetailsVerb { get; set; }

        [VerbOption("getaccountfunds", HelpText = "Get available to bet amount")]
        public AccountFundsSubOptions GetAccountFundsVerb { get; set; }

        [VerbOption("getaccountstatement", HelpText = "Get account statement")]
        public AccountStatementSubOptions GetAccountStatementVerb { get; set; }

        [VerbOption("listcurrencyrates", HelpText = "Returns a list of currency rates")]
        public ListCurrencyRatesSubOptions ListCurrencyRatesVerb { get; set; }

        [VerbOption("listcompetitions", HelpText = "Returns a list of Competitions (i.e., World Cup 2013)")]
        public MarketFilterSubOptions ListCompetitionsVerb { get; set; }

        [VerbOption("listcountries", HelpText = "Returns a list of Countries")]
        public MarketFilterSubOptions ListCountriesVerb { get; set; }

        [VerbOption("listcurrentorders", HelpText = "Returns a list of your current orders")]
        public ListCurrentOrdersSubOptions ListCurrentOrdersVerb { get; set; }

        [VerbOption("listclearedorders", HelpText = "Returns a list of settled bets based on the bet status, ordered by settled date")]
        public ListClearedOrdersSubOptions ListClearedOrdersVerb { get; set; }

        [VerbOption("listevents", HelpText = "Returns a list of Events (i.e, Reading vs. Man United)")]
        public MarketFilterSubOptions ListEventsVerb { get; set; }

        [VerbOption("listeventtypes", HelpText = "Returns a list of Event Types (i.e. Sports)")]
        public MarketFilterSubOptions ListEventTypesVerb { get; set; }

        [VerbOption("listmarketbook", HelpText = "Returns a list of dynamic data about markets")]
        public ListMarketBookSubOptions ListMarketBookVerb { get; set; }

        [VerbOption("listmarketcatalogue", HelpText = "Returns a list of information about markets that does not change (or changes very rarely)")]
        public ListMarketCatalogueSubOptions ListMarketCatalogueVerb { get; set; }

        [VerbOption("listmarketprofitandloss", HelpText = "Retrieve profit and loss for a given list of markets")]
        public ListMarketProfitAndLossSubOptions ListMarketProfitAndLoss { get; set; }

        [VerbOption("selftest", HelpText = "Performs a series of read-only operations to test your settings")]
        public CommonOptions SelfTest { get; set; }

        [HelpVerbOption]
        public string GetUsage(string verb)
        {
            return HelpText.AutoBuild(this, verb);
        }
    }
}