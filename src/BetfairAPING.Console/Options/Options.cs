using CommandLine;

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
    }
}