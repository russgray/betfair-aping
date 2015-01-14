using CommandLine;

namespace BetfairAPING.Console.Options
{
    class AccountStatementSubOptions : CommonOptions
    {
        [Option("from-record", HelpText = "Specifies the first record that will be returned. Records start at index zero. If not specified then it will default to 0")]
        public int FromRecord { get; set; }

        [Option("record-count", HelpText = "Specifies the maximum number of records to be returned. Note that there is a page size limit of 100")]
        public int RecordCount { get; set; }

        [Option("from-days", HelpText = "Days of history to retrieve (from today)")]
        public int FromDays { get; set; }

        [Option("include-item", HelpText = "Which items to include, if not specified then defaults to ALL")]
        public string IncludeItem { get; set; }

        [Option("wallet", HelpText = "Which wallet to return statementItems for. If unspecified then the UK wallet will be selected")]
        public string Wallet { get; set; }
    }
}