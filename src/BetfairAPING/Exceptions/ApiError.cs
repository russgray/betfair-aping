namespace BetfairAPING.Exceptions
{
    [ToString]
    public class ApiError
    {
        public Detail Detail { get; set; }
        public string FaultCode { get; set; }
        public string FaultString { get; set; }
    }
}