using BetfairAPING.Authentication;
using Machine.Fakes;
using Machine.Specifications;

namespace BetfairAPING.Tests.Authentication.CombinedSettings
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable ConvertToLambdaExpression
    public class AuthSettingPriorityCredentialStore : WithSubject<Authenticator>
    {
        Establish context = () =>
        {
            The<ICredentialStore>()
                .WhenToldTo(cs => cs.GetCredentials("betfair/aping"))
                .Return(new UserPass
                {
                    Username = "storeusername",
                    Password = "storepassword",
                });
        };

        protected static LoginCredentials Response;
    }

    [Subject("With config file and credential store available")]
    public class When_config_has_username_but_no_password : AuthSettingPriorityCredentialStore
    {
        Establish context = () =>
        {
            The<IAuthConfigProvider>()
                .WhenToldTo(p => p.ReadConfig())
                .Return(new LoginCredentials { Username = "fileusername" });
        };

        Because of = () => Response = Subject.ResolveCredentials(null, "betfair/aping", null);

        It should_use_config_username = () => Response.Username.ShouldEqual("fileusername");
        It should_use_store_password = () => Response.Password.ShouldEqual("storepassword");
    }

    [Subject("With config file and credential store available")]
    public class When_config_has_password_but_no_username : AuthSettingPriorityCredentialStore
    {
        Establish context = () =>
        {
            The<IAuthConfigProvider>()
                .WhenToldTo(p => p.ReadConfig())
                .Return(new LoginCredentials { Password = "filepassword" });
        };

        Because of = () => Response = Subject.ResolveCredentials(null, "betfair/aping", null);

        It should_use_store_username = () => Response.Username.ShouldEqual("storeusername");
        It should_use_config_password = () => Response.Password.ShouldEqual("filepassword");
    }

    [Subject("With config file and credential store available")]
    public class When_username_is_provided_and_config_has_no_password : AuthSettingPriorityCredentialStore
    {
        Establish context = () =>
        {
            The<IAuthConfigProvider>()
                .WhenToldTo(p => p.ReadConfig())
                .Return(new LoginCredentials { Username = "fileusername" });
        };

        Because of = () => Response = Subject.ResolveCredentials("username", "betfair/aping", null);

        It should_use_provided_username = () => Response.Username.ShouldEqual("username");
        It should_use_store_password = () => Response.Password.ShouldEqual("storepassword");
    }

    [Subject("With config file and credential store available")]
    public class When_username_is_provided_and_config_has_password : AuthSettingPriorityCredentialStore
    {
        Establish context = () =>
        {
            The<IAuthConfigProvider>()
                .WhenToldTo(p => p.ReadConfig())
                .Return(new LoginCredentials { Password = "filepassword" });
        };

        Because of = () => Response = Subject.ResolveCredentials("username", "betfair/aping", null);

        It should_use_provided_username = () => Response.Username.ShouldEqual("username");
        It should_use_config_password = () => Response.Password.ShouldEqual("filepassword");
    }
}