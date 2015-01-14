namespace BetfairAPING.Entities.Accounts
{
    [ToString]
    public class CurrencyRate
    {
        public string CurrencyCode { get; set; }
        public double Rate { get; set; }
    }
}