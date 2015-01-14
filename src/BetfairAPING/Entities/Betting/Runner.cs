using System;
using System.Collections.Generic;

namespace BetfairAPING.Entities.Betting
{
    [ToString]
    public class Runner
    {
        public long SelectionId { get; set; }
        public double Handicap { get; set; }
        public string Status { get; set; }
        public double AdjustmentFactor { get; set; }
        public double LastPriceTraded { get; set; }
        public double TotalMatched { get; set; }
        public DateTime RemovalDate { get; set; }
        // ReSharper disable once InconsistentNaming
        public StartingPrices SP { get; set; }
        // ReSharper disable once InconsistentNaming
        public ExchangePrices EX { get; set; }
        public List<Order> Orders { get; set; }
        public List<Match> Matches { get; set; }
    }
}