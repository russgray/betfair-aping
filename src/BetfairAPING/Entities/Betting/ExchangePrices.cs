using System.Collections.Generic;

namespace BetfairAPING.Entities.Betting
{
    [ToString]
    public class ExchangePrices
    {
        public List<PriceSize> AvailableToBack { get; set; }
        public List<PriceSize> AvailableToLay { get; set; }
        public List<PriceSize> TradedVolume { get; set; }
    }
}