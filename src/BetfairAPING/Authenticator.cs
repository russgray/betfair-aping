using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using BetfairAPING.Entities.Accounts;
using CredentialManagement;
using MethodTimer;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;

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
            // Load credentials from store if provided
            UserPass credential = null;
            if (!string.IsNullOrEmpty(credentialStore))
                credential = _store.GetCredentials(credentialStore);

            var json = _authConfigProvider.ReadConfig();

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

    public class LoginCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string CertPath { get; set; }
        public string AppKey { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Username))
                throw new NullReferenceException("Username");
            if (string.IsNullOrEmpty(Password))
                throw new NullReferenceException("Password");
            if (string.IsNullOrEmpty(CertPath))
                throw new NullReferenceException("CertPath");
            if (string.IsNullOrEmpty(AppKey))
                throw new NullReferenceException("AppKey");
        }
    }

    public interface IAuthConfigProvider
    {
        LoginCredentials ReadConfig();
    }

    internal class FileAuthConfigProvider : IAuthConfigProvider
    {
        public LoginCredentials ReadConfig()
        {
            var homeConfig = Path.Combine(Environment.ExpandEnvironmentVariables("%HOME%"), "bf.json");

            LoginCredentials json = null;
            // Look for config file in current dir first
            if (File.Exists("bf.json"))
                json = JsonConvert.DeserializeObject<LoginCredentials>(File.ReadAllText("bf.json"));
            else if (File.Exists(homeConfig))
                json = JsonConvert.DeserializeObject<LoginCredentials>(File.ReadAllText(homeConfig));

            return json;
        }
    }

    public interface ICredentialStore
    {
        UserPass GetCredentials(string credentialStoreName);
    }

    internal class DefaultCredentialStore : ICredentialStore
    {
        public UserPass GetCredentials(string credentialStoreName)
        {
            var cm = new Credential { Target = credentialStoreName };
            if (!cm.Exists())
                return null;

            cm.Load();
            return new UserPass
                   {
                       Username = cm.Username,
                       Password = cm.Password
                   };
        }
    }

    public class UserPass
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
