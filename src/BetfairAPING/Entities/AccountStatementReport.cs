using System.Collections;
using System.Collections.Generic;

namespace BetfairAPING.Entities
{
    public class AccountStatementReport
    {
        public List<AccountStatementItem> AccountStatement { get; set; }
        public bool MoreAvailable { get; set; }
    }
}