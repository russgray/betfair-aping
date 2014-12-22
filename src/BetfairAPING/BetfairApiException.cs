using System;

namespace BetfairAPING
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

        #region Nested classes for deserialisation

        public class AccountApiExceptionDetails
        {
            public string ErrorDetails { get; set; }
            public string ErrorCode { get; set; }
            public string RequestUuid { get; set; }
        }

        public class Detail
        {
            // ReSharper disable once InconsistentNaming
            public AccountApiExceptionDetails AccountAPINGException { get; set; }
            public string ExceptionName { get; set; }
        }

        public class ApiError
        {
            public Detail Detail { get; set; }
            public string FaultCode { get; set; }
            public string FaultString { get; set; }
        }

        #endregion
    }
}