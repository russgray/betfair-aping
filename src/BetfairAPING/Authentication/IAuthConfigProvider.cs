namespace BetfairAPING.Authentication
{
    public interface IAuthConfigProvider
    {
        LoginCredentials ReadConfig();
    }
}