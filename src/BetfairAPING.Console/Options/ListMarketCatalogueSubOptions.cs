using System.Collections.Generic;
using CommandLine;

namespace BetfairAPING.Console.Options
{
    class ListMarketCatalogueSubOptions : MarketFilterSubOptions
    {
        [Option("market-projections", HelpText = "The type and amount of data returned about the market")]
        public string MarketProjection { get; set; }

        [Option("market-sort", HelpText = "The order of the results. Will default to RANK if not passed")]
        public string MarketSort { get; set; }

        [Option("max-results", DefaultValue= 1000, HelpText = "Limit on the total number of results returned, must be greater than 0 and less than or equal to 1000")]
        public int MaxResults { get; set; }

        internal HashSet<string> MarketProjectionAsSet
        {
            get { return MarketProjection == null ? null : new HashSet<string>(MarketProjection.Split(',')); }
        }
    }
}