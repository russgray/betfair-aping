using CommandLine;
using CommandLine.Text;

namespace BetfairAPING.Console.Options
{
    class AccountFundsSubOptions : CommonOptions
    {
        [Option("wallet", HelpText = "Name of the wallet in question")]
        public string Wallet { get; set; }
    }
}