using System.Collections.Generic;
using CommandLine;

namespace BetfairAPING.Console.Options
{
    class ListMarketCatalogueSubOptions : MarketFilterSubOptions
    {
        [Option("market-projections")]
        public string MarketProjection { get; set; }

        [Option("market-sort")]
        public string MarketSort { get; set; }

        [Option("max-results", Required = true)]
        public int MaxResults { get; set; }

        internal HashSet<string> MarketProjectionAsSet
        {
            get { return MarketProjection == null ? null : new HashSet<string>(MarketProjection.Split(',')); }
        }
    }
}