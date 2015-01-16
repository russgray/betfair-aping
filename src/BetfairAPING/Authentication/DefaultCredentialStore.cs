using CredentialManagement;

namespace BetfairAPING.Authentication
{
    internal class DefaultCredentialStore : ICredentialStore
    {
        public UserPass GetCredentials(string credentialStoreName)
        {
            var cm = new Credential { Target = credentialStoreName };
            if (!cm.Exists())
                return null;

            cm.Load();
            return new UserPass
                   {
                       Username = cm.Username,
                       Password = cm.Password
                   };
        }
    }
}