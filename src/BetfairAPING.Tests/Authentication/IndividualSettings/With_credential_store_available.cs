using BetfairAPING.Authentication;
using Machine.Fakes;
using Machine.Specifications;

namespace BetfairAPING.Tests.Authentication.IndividualSettings
{
    // ReSharper disable InconsistentNaming
    public class AuthSettingPriorityCredentialStore : WithSubject<Authenticator>
    {
        Establish context = () =>
        {
            // ReSharper disable once ConvertToLambdaExpression
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
    
    [Subject("With credential store available")]
    public class A_provided_username : AuthSettingPriorityCredentialStore
    {
        Because of = () => Response = Subject.ResolveCredentials("username", "betfair/aping", null);

        It should_take_priority = () => Response.Username.ShouldEqual("username");
    }

    [Subject("With credential store available")]
    public class Given_no_provided_username_a_credential_store_username : AuthSettingPriorityCredentialStore
    {
        Because of = () => Response = Subject.ResolveCredentials(null, "betfair/aping", null);

        It should_be_used = () => Response.Username.ShouldEqual("storeusername");
    }

    #region Password

    [Subject("With credential store available")]
    public class A_credential_store_password : AuthSettingPriorityCredentialStore
    {
        // If no appkey is provided but one is in the config file, it should be used
        Because of = () => Response = Subject.ResolveCredentials(null, "betfair/aping", null, null);

        It should_be_used = () => Response.Password.ShouldEqual("storepassword");
    }

    #endregion

}