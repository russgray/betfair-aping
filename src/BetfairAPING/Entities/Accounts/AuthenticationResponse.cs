using System.Diagnostics;

namespace BetfairAPING.Entities.Accounts
{
    [DebuggerDisplay("Token = {SessionToken}, Status = {LoginStatus}")]
    public class AuthenticationResponse
    {
        public string SessionToken { get; set; }
        public string LoginStatus { get; set; }
    }
}