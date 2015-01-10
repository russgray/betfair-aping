using CommandLine;

namespace BetfairAPING.Console.Options
{
    abstract class CommonOptions
    {
        [Option('u', "username", MutuallyExclusiveSet = "interactive login", HelpText = "Betfair username")]
        public string Username { get; set; }

        [Option("cert-file", Required = true, HelpText = "Path to Betfair API certificate")]
        public string CertPath { get; set; }

        [Option("app-key", Required = true, HelpText = "Betfair app key")]
        public string AppKey { get; set; }

        [Option('c', "credential-store-name", MutuallyExclusiveSet = "credential login")]
        public string CredentialStoreName { get; set; }

        [Option('v', "verbose", HelpText = "Print detailed information")]
        public bool Verbosity { get; set; }

        [Option("locale")]
        public string Locale { get; set; }
    }
}