using Machine.Fakes;
using Machine.Specifications;

namespace BetfairAPING.Tests
{
    // ReSharper disable InconsistentNaming
    [Subject(typeof(Authenticator))]
    public class When_authenticating_with_provided_settings_only : WithSubject<Authenticator>
    {
        Because of = () => Response = Subject.ResolveCredentials("username", null, "certpath");

        It should_use_provided_username = () => Response.Username.ShouldEqual("username");
        It should_find_no_password = () => Response.Password.ShouldBeNull();
        It should_use_provided_certpath = () => Response.CertPath.ShouldEqual("certpath");

        static LoginCredentials Response;
    }
}
