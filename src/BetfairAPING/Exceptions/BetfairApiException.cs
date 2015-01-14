using System;

namespace BetfairAPING.Exceptions
{
    public class BetfairApiException : ApplicationException
    {
        public ApiError Error { get; private set; }

        public BetfairApiException(ApiError error)
        {
            Error = error;
        }

        public BetfairApiException(string message, ApiError error) : base(message)
        {
            Error = error;
        }

        public override string ToString()
        {
            return string.Format("Ex: Error={0}, Message={1}", Error, Message);
        }
    }
}