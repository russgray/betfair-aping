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

        [HelpVerbOption]
        public string GetUsage(string verb)
        {
            return HelpText.AutoBuild(this, verb);
        }
    }
}