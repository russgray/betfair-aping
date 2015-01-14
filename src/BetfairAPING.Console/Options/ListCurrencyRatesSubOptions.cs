using CommandLine;

namespace BetfairAPING.Console.Options
{
    class ListCurrencyRatesSubOptions : CommonOptions
    {
        [Option("from-currency", HelpText = "The currency from which the rates are computed")]
        public string FromCurrency { get; set; }
    }
}