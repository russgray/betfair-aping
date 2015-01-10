using System;

namespace BetfairAPING.Entities.Betting
{
    public class Match
    {
        public string BetId { get; set; }
        public string MatchId { get; set; }
        public string Side { get; set; }
        public double Price { get; set; }
        public double Size { get; set; }
        public DateTime MatchDate { get; set; }
    }
}