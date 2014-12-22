using System.Diagnostics;

namespace BetfairAPING.Entities.Accounts
{
    [DebuggerDisplay("Name={FirstName}, Surname={LastName}, TZ={Timezone}, Currency={CurrencyCode}")]
    public class AccountDetails
    {
        public string CurrencyCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LocaleCode { get; set; }
        public string Region { get; set; }
        public string Timezone { get; set; }
        public decimal DiscountRate { get; set; }
        public int PointsBalance { get; set; }
    }
}