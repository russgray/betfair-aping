using System.Collections.Generic;
using CommandLine;

namespace BetfairAPING.Console.Options
{
    public class CommonOptions
    {
        [Option('u', "username", MutuallyExclusiveSet = "interactive login", HelpText = "Betfair username")]
        public string Username { get; set; }

        [Option("cert-file", HelpText = "Path to Betfair API certificate")]
        public string CertPath { get; set; }

        [Option("app-key", HelpText = "Betfair app key")]
        public string AppKey { get; set; }

        [Option('c', "credential-store-name", MutuallyExclusiveSet = "credential login", HelpText = "Name of credential entry in Credential Manager")]
        public string CredentialStoreName { get; set; }

        [Option('v', "verbose", HelpText = "Print detailed information")]
        public bool Verbose { get; set; }

        [Option("locale", HelpText = "The language to be used where applicable. If not specified, the customer account default is returned")]
        public string Locale { get; set; }

        [Option("perf-report", HelpText = "Print a performance report at the end of the process")]
        public bool PerfReport { get; set; }

        [Option("json-output", HelpText = "Print responses in JSON format")]
        public bool JsonOutput { get; set; }

        protected static HashSet<string> CommandLineListToSet(string option)
        {
            return option == null ? null : new HashSet<string>(option.Split(','));
        }
    }
}