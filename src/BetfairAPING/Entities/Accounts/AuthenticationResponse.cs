namespace BetfairAPING.Entities.Accounts
{
    [ToString]
    public class AuthenticationResponse
    {
        public string SessionToken { get; set; }
        public string LoginStatus { get; set; }
    }
}