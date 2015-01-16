using Machine.Fakes;
using Machine.Specifications;

namespace BetfairAPING.Tests
{
    // ReSharper disable InconsistentNaming
    [Subject(typeof(Authenticator))]
    public class When_authenticating_with_provided_settings_and_credential_store : WithSubject<Authenticator>
    {
        Establish context = () =>
                            {
                                // ReSharper disable once ConvertToLambdaExpression
                                The<ICredentialStore>()
                                    .WhenToldTo(cs => cs.GetCredentials("betfair/aping"))
                                    .Return(new UserPass {Username = "storeusername", Password = "storepassword"});
                            };

        Because of = () => Response = Subject.ResolveCredentials("username", "betfair/aping", "path");

        It should_prefer_provided_settings = () => Response.Username.ShouldEqual("username");
        It should_find_unspecified_settings_in_store = () => Response.Password.ShouldEqual("storepassword");

        static LoginCredentials Response;
    }
}