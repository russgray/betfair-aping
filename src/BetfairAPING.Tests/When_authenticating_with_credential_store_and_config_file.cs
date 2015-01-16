using Machine.Fakes;
using Machine.Specifications;

namespace BetfairAPING.Tests
{
    // ReSharper disable InconsistentNaming
    [Subject(typeof(Authenticator))]
    public class When_authenticating_with_credential_store_and_config_file : WithSubject<Authenticator>
    {
        Establish context = () =>
        {
            The<ICredentialStore>()
                .WhenToldTo(cs => cs.GetCredentials("betfair/aping"))
                .Return(new UserPass { Username = "storeusername", Password = "storepassword" });
            
            The<IAuthConfigProvider>()
                .WhenToldTo(p => p.ReadConfig())
                .Return(new LoginCredentials { Username = "fileusername", CertPath = "filecertpath" });
        };

        Because of = () => Response = Subject.ResolveCredentials(null, "betfair/aping", null);

        It should_prefer_config_settings = () => Response.Username.ShouldEqual("fileusername");
        It should_find_unspecified_settings_in_store = () => Response.Password.ShouldEqual("storepassword");

        static LoginCredentials Response;
    }
}