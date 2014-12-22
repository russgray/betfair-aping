using System.Diagnostics;

namespace BetfairAPING.Entities
{
    [DebuggerDisplay("Available={AvailableToBetBalance}, Exposure={Exposure}/{ExposureLimit}, Wallet={Wallet}")]
    public class AccountFunds
    {
        public decimal AvailableToBetBalance { get; set; }
        public decimal Exposure { get; set; }
        public decimal RetainedCommission { get; set; }
        public decimal ExposureLimit { get; set; }
        public decimal DiscountRate { get; set; }
        public int PointsBalance { get; set; }
        public string Wallet { get; set; }
    }
}