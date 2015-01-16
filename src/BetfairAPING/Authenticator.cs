using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using BetfairAPING.Authentication;
using BetfairAPING.Entities.Accounts;
using MethodTimer;
using RestSharp;

namespace BetfairAPING
{
    public class Authenticator
    {
        private readonly IAuthConfigProvider _authConfigProvider;
        private readonly ICredentialStore _store;
        private readonly RestClient _client = new RestClient("https://identitysso.betfair.com/api/certlogin");

        public Authenticator(ICredentialStore store = null, IAuthConfigProvider authConfigProvider = null)
        {
            _authConfigProvider = authConfigProvider ?? new FileAuthConfigProvider();
            _store = store ?? new DefaultCredentialStore();
        }

        [Time]
        public async Task<AuthenticationResponse> Authenticate(LoginCredentials credentials)
        {
            credentials.Validate();
            var req = new RestRequest(Method.POST);

            req.AddHeader("X-Application", "betfair-aping");
            req.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            req.AddParameter("username", credentials.Username);
            req.AddParameter("password", credentials.Password);

            var cert = new X509Certificate2(credentials.CertPath);
            _client.ClientCertificates = new X509Certificate2Collection(cert);

            var resp = await _client.ExecutePostTaskAsync<AuthenticationResponse>(req);
            return resp.Data;
        }

        /// <summary>
        /// Priority is command-line, bf.json, credential store. If not found, application 
        /// should prompt interactively
        /// </summary>
        public LoginCredentials ResolveCredentials(
            string username = null, 
            string credentialStore = null, 
            string certPath = null,
            string appKey = null)
        {
            var json = _authConfigProvider.ReadConfig();

            // Use credential store from config if necessary
            if (credentialStore == null && json != null)
                credentialStore = json.CredentialStore;

            // Load credentials from store if provided
            UserPass credential = null;
            if (!string.IsNullOrEmpty(credentialStore))
                credential = _store.GetCredentials(credentialStore);

            return new LoginCredentials
                   {
                       Username = FindUsername(username, credential, json),
                       Password = FindPassword(credential, json),
                       CertPath = FindCertPath(certPath, json),
                       AppKey = FindAppKey(appKey, json)
                   };
        }

        private static string FindUsername(string username = null, UserPass credential = null, LoginCredentials json = null)
        {
            // If we were provided a username, use it
            if (!string.IsNullOrEmpty(username))
                return username;

            // If there's a username in json config, use it
            if (json != null && !string.IsNullOrEmpty(json.Username))
                return json.Username;

            // If a credential store was specified, use it
            if (credential != null)
                return credential.Username;

            // Uh-oh
            return null;
        }

        private static string FindPassword(UserPass credential = null, LoginCredentials json = null)
        {
            // If there's a password in json config, use it
            if (json != null && !string.IsNullOrEmpty(json.Password))
                return json.Password;

            // If a credential store was specified, use it
            if (credential != null)
                return credential.Password;

            // Uh-oh
            return null;
        }

        private static string FindCertPath(string certPath = null, LoginCredentials json = null)
        {
            // If we were provided a path, use it
            if (!string.IsNullOrEmpty(certPath))
                return certPath;

            // If there's a path in json config, use it
            if (json != null && !string.IsNullOrEmpty(json.CertPath))
                return json.CertPath;

            // Uh-oh
            return null;
        }

        private static string FindAppKey(string appKey = null, LoginCredentials json = null)
        {
            // If we were provided a key, use it
            if (!string.IsNullOrEmpty(appKey))
                return appKey;

            // If there's a key in json config, use it
            if (json != null && !string.IsNullOrEmpty(json.AppKey))
                return json.AppKey;

            // Uh-oh
            return null;
        }
    }
}
