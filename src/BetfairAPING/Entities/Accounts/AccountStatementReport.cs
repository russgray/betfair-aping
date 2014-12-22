using System.Collections.Generic;

namespace BetfairAPING.Entities.Accounts
{
    public class AccountStatementReport
    {
        public List<AccountStatementItem> AccountStatement { get; set; }
        public bool MoreAvailable { get; set; }
    }
}