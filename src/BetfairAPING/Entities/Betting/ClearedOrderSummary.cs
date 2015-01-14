using System;

namespace BetfairAPING.Entities.Betting
{
    [ToString]
    public class ClearedOrderSummary
    {
        public string EventTypeId { get; set; }
        public string EventId { get; set; }
        public string MarketId { get; set; }
        public long SelectionId { get; set; }
        public double Handicap { get; set; }
        public string BetId { get; set; }
        public DateTime PlacedDate { get; set; }
        public string PersistenceType { get; set; }
        public string OrderType { get; set; }
        public string Side { get; set; }
        public ItemDescription ItemDescription { get; set; }
        public double Price { get; set; }
        public DateTime SettledDate { get; set; }
        public int BetCount { get; set; }
        public double Commission { get; set; }
        public double PriceMatched { get; set; }
        public bool PriceReduced { get; set; }
        public double SizeSettled { get; set; }
        public double Profit { get; set; }
        public double SizeCancelled { get; set; }
    }
}