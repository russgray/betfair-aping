using BetfairAPING.Authentication;
using Machine.Fakes;
using Machine.Specifications;

namespace BetfairAPING.Tests.Authentication.IndividualSettings
{
    // ReSharper disable InconsistentNaming
    public class AuthSettingPriorityCredentialStoreAndConfigFile : WithSubject<Authenticator>
    {
        Establish context = () =>
        {
            The<IAuthConfigProvider>()
                .WhenToldTo(p => p.ReadConfig())
                .Return(new LoginCredentials
                {
                    Username = "fileusername",
                    Password = "filepassword",
                    AppKey = "fileappkey",
                    CertPath = "filecertpath",
                });
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

    #region Username

    [Subject("With config file and credential store available")]
    public class Given_a_provided_username : AuthSettingPriorityCredentialStoreAndConfigFile
    {
        Because of = () => Response = Subject.ResolveCredentials("username", "betfair/aping", null);

        It should_be_used = () => Response.Username.ShouldEqual("username");
        It should_override_credential_store = () => Response.Username.ShouldNotEqual("storeusername");
        It should_override_config = () => Response.Username.ShouldNotEqual("fileusername");
    }

    [Subject("With config file and credential store available")]
    public class Given_no_provided_username_a_config_file_username : AuthSettingPriorityCredentialStoreAndConfigFile
    {
        Because of = () => Response = Subject.ResolveCredentials(null, "betfair/aping", null);

        It a_config_file_username_should_be_used = () => Response.Username.ShouldEqual("fileusername");
        It a_config_file_username_should_override_credential_store = () => Response.Username.ShouldNotEqual("storeusername");
    }

    #endregion

    #region Password

    [Subject("With config file and credential store available")]
    public class A_config_file_password : AuthSettingPriorityCredentialStoreAndConfigFile
    {
        // If no appkey is provided but one is in the config file, it should be used
        Because of = () => Response = Subject.ResolveCredentials(null, "betfair/aping", null, null);

        It should_be_used = () => Response.Password.ShouldEqual("filepassword");
    }

    #endregion

    #region AppKey

    [Subject("With config file and credential store available")]
    public class Given_a_provided_appkey : AuthSettingPriorityCredentialStoreAndConfigFile
    {
        Because of = () => Response = Subject.ResolveCredentials(null, "betfair/aping", null, "appkey");

        It should_be_used = () => Response.AppKey.ShouldEqual("appkey");
        It should_override_config = () => Response.AppKey.ShouldNotEqual("fileappkey");
    }

    [Subject("With config file and credential store available")]
    public class Given_no_provided_appkey_a_config_file_appkey : AuthSettingPriorityCredentialStoreAndConfigFile
    {
        // If no appkey is provided but one is in the config file, it should be used
        Because of = () => Response = Subject.ResolveCredentials(null, "betfair/aping", null, null);

        It should_be_used = () => Response.AppKey.ShouldEqual("fileappkey");
    }

    #endregion

    #region CertPath

    [Subject("With config file and credential store available")]
    public class Given_a_provided_certpath : AuthSettingPriorityCredentialStoreAndConfigFile
    {
        Because of = () => Response = Subject.ResolveCredentials(null, "betfair/aping", "certpath", null);

        It should_be_used = () => Response.CertPath.ShouldEqual("certpath");
        It should_override_config = () => Response.AppKey.ShouldNotEqual("filecertpath");
    }

    [Subject("With config file and credential store available")]
    public class Given_no_provided_certpath_a_config_certpath : AuthSettingPriorityCredentialStoreAndConfigFile
    {
        Because of = () => Response = Subject.ResolveCredentials(null, "betfair/aping", "certpath", null);

        It should_be_used = () => Response.CertPath.ShouldEqual("certpath");
    }

    #endregion
}
