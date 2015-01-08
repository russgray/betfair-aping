using CommandLine;

namespace BetfairAPING.Console.Options
{
    abstract class CommonOptions
    {
        [Option('u', "username", HelpText = "Betfair username")]
        public string Username { get; set; }

        [Option("cert-file", HelpText = "Path to Betfair API certificate")]
        public string CertPath { get; set; }

        [Option("app-key", HelpText = "Betfair app key")]
        public string AppKey { get; set; }

        [Option('v', "verbose", HelpText = "Print detailed information")]
        public bool Verbosity { get; set; }
    }
}