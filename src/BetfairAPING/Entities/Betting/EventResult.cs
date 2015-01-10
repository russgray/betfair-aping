using RestSharp.Serializers;

namespace BetfairAPING.Entities.Betting
{
    public class EventResult
    {
        public Event Event { get; set; }
        public int MarketCount { get; set; }

        public override string ToString()
        {
            var s = new JsonSerializer();
            return s.Serialize(this);
        }
    }
}