namespace BetfairAPING.Exceptions
{
    [ToString]
    public class AccountApiExceptionDetails
    {
        public string ErrorDetails { get; set; }
        public string ErrorCode { get; set; }
        public string RequestUuid { get; set; }
    }
}