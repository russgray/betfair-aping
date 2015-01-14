namespace BetfairAPING.Exceptions
{
    [ToString]
    public class Detail
    {
        // ReSharper disable once InconsistentNaming
        public AccountApiExceptionDetails AccountAPINGException { get; set; }
        public string ExceptionName { get; set; }
    }
}