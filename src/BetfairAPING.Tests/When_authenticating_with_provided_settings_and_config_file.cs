using Machine.Fakes;
using Machine.Specifications;

namespace BetfairAPING.Tests
{
    // ReSharper disable InconsistentNaming
    [Subject(typeof(Authenticator))]
    public class When_authenticating_with_provided_settings_and_config_file : WithSubject<Authenticator>
    {
        Establish context = () =>
                            {
                                // ReSharper disable once ConvertToLambdaExpression
                                The<IAuthConfigProvider>()
                                    .WhenToldTo(p => p.ReadConfig())
                                    .Return(new LoginCredentials { Username = "fileusername", Password = "filepassword", CertPath = "filecertpath" });
                            };

        Because of = () => Response = Subject.ResolveCredentials("username", null, null);

        It should_prefer_provided_settings = () => Response.Username.ShouldEqual("username");
        It should_find_unspecified_settings_in_config = () => Response.Password.ShouldEqual("filepassword");

        static LoginCredentials Response;
    }
}