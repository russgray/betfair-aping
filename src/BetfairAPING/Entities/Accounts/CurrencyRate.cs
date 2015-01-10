using System.Diagnostics;
using RestSharp.Serializers;

namespace BetfairAPING.Entities.Accounts
{
    [DebuggerDisplay("CurrencyCode = {CurrencyCode}, Rate = {Rate}")]
    public class CurrencyRate
    {
        public string CurrencyCode { get; set; }
        public double Rate { get; set; }

        public override string ToString()
        {
            var s = new JsonSerializer();
            return s.Serialize(this);
        }
    }
}