using CommandLine;

namespace BetfairAPING.Console.Options
{
    class AccountStatementSubOptions : CommonOptions
    {
        [Option("record-count")]
        public int RecordCount { get; set; }

        [Option("from-days")]
        public int FromDays { get; set; }
    }
}