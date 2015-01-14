using System;
using System.Collections.Generic;

namespace BetfairAPING.Entities.Accounts
{
    [ToString]
    public class AccountStatementItem
    {
        public string RefId { get; set; }
        public DateTime ItemDate { get; set; }
        public double Amount { get; set; }
        public double Balance { get; set; }
        public string ItemClass { get; set; }
        public Dictionary<string, string> ItemClassData { get; set; }
        public StatementLegacyData LegacyData { get; set; }
    }
}