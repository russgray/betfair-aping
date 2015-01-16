namespace BetfairAPING.Authentication
{
    public interface ICredentialStore
    {
        UserPass GetCredentials(string credentialStoreName);
    }
}