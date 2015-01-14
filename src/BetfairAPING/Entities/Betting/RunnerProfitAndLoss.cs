namespace BetfairAPING.Entities.Betting
{
    [ToString]
    public class RunnerProfitAndLoss
    {
        public long SelectionId { get; set; }
        public double IfWin { get; set; }
        public double IfLose { get; set; }
    }
}