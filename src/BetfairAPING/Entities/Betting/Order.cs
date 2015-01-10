using System;

namespace BetfairAPING.Entities.Betting
{
    public class Order
    {
        public string BetId { get; set; }
        public string OrderType { get; set; }
        public string Status { get; set; }
        public string PersistenceType { get; set; }
        public string Side { get; set; }
        public double Price { get; set; }
        public double Size { get; set; }
        public double BspLiability { get; set; }
        public DateTime PlacedDate { get; set; }
        public double AvgPriceMatched { get; set; }
        public double SizeMatched { get; set; }
        public double SizeRemaining { get; set; }
        public double SizeLapsed { get; set; }
        public double SizeCancelled { get; set; }
        public double SizeVoided { get; set; }

    }
}