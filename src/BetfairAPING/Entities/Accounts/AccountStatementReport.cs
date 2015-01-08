using System.Collections.Generic;
using RestSharp.Serializers;

namespace BetfairAPING.Entities.Accounts
{
    public class AccountStatementReport
    {
        public List<AccountStatementItem> AccountStatement { get; set; }
        public bool MoreAvailable { get; set; }

        public override string ToString()
        {
            var s = new JsonSerializer();
            return s.Serialize(this);
        }
    }
}