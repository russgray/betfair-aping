using System.Diagnostics;

namespace BetfairAPING.Entities.Accounts
{
    [DebuggerDisplay("CurrencyCode = {CurrencyCode}, Rate = {Rate}")]
    public class CurrencyRate
    {
        public string CurrencyCode { get; set; }
        public double Rate { get; set; }
    }
}