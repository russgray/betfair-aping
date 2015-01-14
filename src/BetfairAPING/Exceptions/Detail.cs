namespace BetfairAPING.Exceptions
{
    [ToString]
    public class Detail
    {
        public string ExceptionName { get; set; }
    }

    [ToString]
    public class AccountApiExceptionDetail : Detail
    {
        // ReSharper disable once InconsistentNaming
        public APINGException AccountAPINGException { get; set; }
    }

    [ToString]
    public class BettingApiExceptionDetail : Detail
    {
        // ReSharper disable once InconsistentNaming
        public APINGException BettingAPINGException { get; set; }
    }
}