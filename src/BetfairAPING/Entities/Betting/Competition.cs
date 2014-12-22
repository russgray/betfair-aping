using System.Diagnostics;

namespace BetfairAPING.Entities.Betting
{
    [DebuggerDisplay("Id = {Id}, Name = {Name}")]
    public class Competition
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}