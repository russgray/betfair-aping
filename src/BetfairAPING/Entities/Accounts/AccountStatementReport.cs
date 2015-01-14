using System.Collections.Generic;

namespace BetfairAPING.Entities.Accounts
{
    [ToString]
    public class AccountStatementReport
    {
        public List<AccountStatementItem> AccountStatement { get; set; }
        public bool MoreAvailable { get; set; }
    }
}