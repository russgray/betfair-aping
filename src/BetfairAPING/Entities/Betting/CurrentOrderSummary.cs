using System;

namespace BetfairAPING.Entities.Betting
{
    [ToString]
    public class CurrentOrderSummary
    {
        public string BetId { get; set; }
        public string MarketId { get; set; }
        public long SelectionId { get; set; }
        public double Handicap { get; set; }
        public PriceSize PriceSize { get; set; }
        public double BspLiability { get; set; }
        public string Side { get; set; }
        public string Status { get; set; }
        public string PersistenceType { get; set; }
        public string OrderType { get; set; }
        public DateTime PlacedDate { get; set; }
        public DateTime MatchedDate { get; set; }
        public double AveragePriceMatched { get; set; }
        public double SizeMatched { get; set; }
        public double SizeRemaining { get; set; }
        public double SizeLapsed { get; set; }
        public double SizeCancelled { get; set; }
        public double SizeVoided { get; set; }
        public string RegulatorAuthCode { get; set; }
        public string RegulatorCode { get; set; }
    }
}