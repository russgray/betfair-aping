using CommandLine;

namespace BetfairAPING.Console.Options
{
    class ListCurrencyRatesSubOptions : CommonOptions
    {
        [Option("from-currency")]
        public string FromCurrency { get; set; }
    }
}