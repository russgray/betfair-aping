namespace BetfairAPING.Exceptions
{
    [ToString]
    public class ApiError
    {
        public string FaultCode { get; set; }
        public string FaultString { get; set; }
    }

    [ToString]
    public class BettingApiError : ApiError
    {
        public BettingApiExceptionDetail Detail { get; set; }
    }

    [ToString]
    public class AccountsApiError : ApiError
    {
        public AccountApiExceptionDetail Detail { get; set; }
    }
}