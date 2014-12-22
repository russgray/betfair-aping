using System;

namespace BetfairAPING.Entities
{
    public class StatementLegacyData
    {
        public double? AvgPrice { get; set; }
        public double BetSize { get; set; }
        public string BetType { get; set; }
        public string BetCategoryType { get; set; }
        public string CommissionRate { get; set; }
        public long EventId { get; set; }
        public long EventTypeId { get; set; }
        public string FullMarketName { get; set; }
        public double GrossBetAmount { get; set; }
        public string MarketName { get; set; }
        public string MarketType { get; set; }
        public DateTime PlacedDate { get; set; }
        public long SelectionId { get; set; }
        public string SelectionName { get; set; }
        public DateTime StartDate { get; set; }
        public string TransactionType { get; set; }
        public long TransactionId { get; set; }
        public string WinLose { get; set; }
    }
}