using System;
using System.Collections.Generic;
using RestSharp.Serializers;

namespace BetfairAPING.Entities.Betting
{
    public class MarketCatalogue
    {
        public string MarketId { get; set; }
        public string MarketName { get; set; }
        public DateTime MarketStartTime { get; set; }
        public MarketDescription Description { get; set; }
        public double TotalMatched { get; set; }
        public List<RunnerCatalogue> Runners { get; set; }
        public EventType EventType { get; set; }
        public Competition Competition { get; set; }
        public Event Event { get; set; }

        public override string ToString()
        {
            var s = new JsonSerializer();
            return s.Serialize(this);
        }
    }
}