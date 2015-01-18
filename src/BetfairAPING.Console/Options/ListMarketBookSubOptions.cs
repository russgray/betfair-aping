using System;
using System.Collections.Generic;
using CommandLine;

namespace BetfairAPING.Console.Options
{
    class ListMarketBookSubOptions : CommonOptions
    {
        [Option("market-ids", Required = true, HelpText = "One or more market ids. The number of markets returned depends on the amount of data you request via the price projection")]
        public string MarketIds { get; set; }

        [Option("price-projection", HelpText = "NOT IMPLEMENTED! The projection of price data you want to receive in the response")]
        public string PriceProjection
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        [Option("order-projection", HelpText = "The orders you want to receive in the response")]
        public string OrderProjection { get; set; }

        [Option("match-projection", HelpText = "If you ask for orders, specifies the representation of matches")]
        public string MatchProjection { get; set; }
    
        internal HashSet<string> MarketIdsAsSet
        {
            get { return MarketIds == null ? null : new HashSet<string>(MarketIds.Split(',')); }
        }
    }
}